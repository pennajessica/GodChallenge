using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GodChallenge.Domain;
using System;
using System.Threading;
using Buscas.Grafos;
using GodChallenge.Skills;

[RequireComponent(typeof(CharacterController))]
public class RTSController : MonoBehaviour {
    public int PID { get; set; }
    public string playerName;
    public int hp;
    public float speed = 20.0f;
    public float height = 90.0f;

    public AudioClip[] hudSounds;

    public GameObject particleClick;
    public Camera myCamera;
    public Nodo nodoDestino;
    public bool canMoveCamera = true;
    public bool freezePlayer = false;
    public HudOptions hudOptions;

    protected Animator _animator;
    private Player _player;

    public CharacterType characterType;

    [HideInInspector()]
    public Vector3 targetPosition;
    [HideInInspector]
    public Vector3 myMousePosition;
    private CharacterController _controller;
    private float _fixedRotateZ;
    private float _fixedRotateX;
    private bool skillCasted;

    // Pathfinding
    private List<Nodo> pathNodes;
    private bool moveByPath = false;
    private int actualNode = 0;
    public bool isOffline = false;
    public float nodeDistance = 20.0f;

    public List<GameObject> skillsInitialPositions;

    public Player GetPlayer {
        get { return this._player; }
    }

    public int shopIndex;
    public float x, y, h, w;

    internal void Awake() {
        if (!this.isOffline)
            this.isOffline = (Application.loadedLevelName == "Tutorial");

        if (networkView.isMine || this.isOffline) {
            this._fixedRotateX = this.transform.rotation.x;
            this._fixedRotateZ = this.transform.rotation.z;
            this._controller = this.GetComponent<CharacterController>();
            this._player = SelectionPlayer.getPlayer(characterType.GetHashCode());
            this._player.Controller = this.gameObject;
            this._player.DefaultSpeed = this.speed;
            this._player.ActualSpeed = this._player.DefaultSpeed;
            this._animator = this.GetComponentInChildren<Animator>();
            this.targetPosition = this.transform.position;

            if (!_animator)
                Debug.Log("The character you would like to control doesn't have animations.");
        }

        this._player.Hp = hp;
    }

    void Update() {
        if (networkView.isMine || this.isOffline) {
            Options();

            if (!this.freezePlayer) {
                SelectArea();
                Move();
                OnSkillClick();
            }

            if (this.skillCasted) {
                this._player.State = CharacterState.IDLE;
                //this._animator.SetInteger("Action", this._player.State.GetHashCode());
            }
            this.speed = this._player.ActualSpeed;

            if (this._player.State == CharacterState.DEAD) {
                this.freezePlayer = true;
            }

            this.myMousePosition = Input.mousePosition;

            this._animator.SetInteger("Action", this._player.State.GetHashCode());
        }
        //Debug.Log("speed: " + this._animator.speed);
        //Debug.Log(this._player.State);
        //if (this.speed != this._player.DefaultSpeed && !this._player.Slowed) {
        //    this.speed = this._player.DefaultSpeed;
        //}
    }

    private void Options() {
        if (!hudOptions)
            return;

        if (Input.GetButtonDown("Options")) {
            this.freezePlayer = !this.freezePlayer;
            hudOptions.showHud = !hudOptions.showHud;
            //float left = (Screen.width / 2) - ((hudOptions.width * w) / 2);
            //float top = y;
            //Rect optRect = new Rect(left + x, top, hudOptions.width * w, hudOptions.height * h);
            //GUI.DrawTexture(optRect, hudOptions);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == "DeadZone") {
            this._player.applyDeadZone();
        }
    }

    [RPC]
    public void Colisao(Skill skill) {
        Debug.Log("skill: " + skill.name);
    }

    void OnTriggerEnter(Collider hit) {
        if (networkView.isMine || this.isOffline) {
            Debug.Log(hit.tag);
            if (hit.gameObject.tag == "Skill") {
                SkillBehaviour skillBehaviour = hit.GetComponent<SkillBehaviour>();
                if (skillBehaviour.fromPlayer != this._player) {

                    //Debug.Log("Nome: " + skillBehaviour.skill.Name + " Dano: " + skillBehaviour.skill.Damage);

                    //this._animator.SetBool("Apanhando", true);


                    skillBehaviour.skill.ApplySkillEffect(this._player, skillBehaviour.from);

                    Network.Destroy(skillBehaviour.gameObject);
                    this._player.State = CharacterState.TAKING_HIT;
                    float animationLength = this._animator.GetCurrentAnimatorStateInfo(0).length;
                    new Thread(() => {
                        Thread.Sleep(TimeSpan.FromSeconds(animationLength));
                        this._player.State = CharacterState.IDLE;
                    });
                }
            }
        }
    }

    /// <summary>
    /// Verifica se a skill foi selecionada.
    /// Se foi, chama o método para instanciá-la.
    /// </summary>
    private void OnSkillClick() {
        if (Input.GetButton("1st Skill")) {
            if (this._player.SkillList[0] == null) {
                Debug.Log("This player doesn't have the 1st Skill");
                return;
            } else {
                castSkill(0);
            }
        } else if (Input.GetButton("2nd Skill")) {
            if (this._player.SkillList[1] == null) {
                Debug.Log("This player doesn't have the 2nd Skill");
                return;
            } else {
                this.castSkill(1);
            }
        } else if (Input.GetButton("3rd Skill")) {
            if (this._player.SkillList[2] == null) {
                Debug.Log("This player doesn't have the 3rd Skill");
                return;
            } else {
                this.castSkill(2);
            }
        } else if (Input.GetButton("4th Skill")) {
            if (this._player.SkillList[3] == null) {
                Debug.Log("This player doesn't have the 4th Skill");
                return;
            } else {
                this.castSkill(3);
            }
        } else if (Input.GetButton("5th Skill")) {
            if (this._player.SkillList[4] == null) {
                Debug.Log("This player doesn't have the 5th Skill");
                return;
            } else {
                this.castSkill(4);
            }
        }
    }

    /// <summary>
    /// Instancia a Skill selecionada.
    /// </summary>
    /// <param name="index"></param>
    private void castSkill(int index) {
        this.audio.clip = hudSounds[0];
        this.audio.Play();
        if (this._player.SkillList[index].CastingTime > 0) {
            this.freezePlayer = true;
            this.skillCasted = false;
            new Thread(() => {
                Thread.Sleep(TimeSpan.FromMilliseconds(this._player.SkillList[index].CastingTime));
                this.freezePlayer = false;
                this.skillCasted = true;
                this._player.State = CharacterState.IDLE;

            }).Start();
        }
        if (this._player.SkillList[index].CastSkill(this)) {
            this._player.State = CharacterState.CASTING;
            //this._animator.SetInteger("Action", this._player.State.GetHashCode());
            this._animator.SetInteger("SkillIndex", index);
        }
    }

    /// <summary>
    /// Método de movimentação do personagem.
    /// Se houver colisão entre o Raycast da camera e a posição do mouse
    /// será feita uma busca de caminhos para ele poder ir até o local correto.
    /// </summary>
    private void Move() {
        if (Input.GetButtonDown("MoveTo")) {
            this._player.State = CharacterState.WALKING;

            //this._animator.SetInteger("Action", this._player.State.GetHashCode());

            this.moveByPath = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitdist = Vector3.Distance(Input.mousePosition, Camera.main.transform.position);
            RaycastHit[] hitAll;
            if ((hitAll = Physics.RaycastAll(ray)) != null) {
                RaycastHit hit;

                try {
                    hit = hitAll.Where(linq => linq.transform.gameObject.tag == "Terrain").First();
                } catch (Exception) {
                    return;
                }

                RaycastHit colisionHit;
                if (Physics.Linecast(this.transform.position, hit.point, out colisionHit) && colisionHit.collider.tag != "Terrain") {
                    MovePath(hit.point);
                    return;
                }

                if (particleClick != null)
                    Instantiate(particleClick, hit.point, Camera.main.transform.rotation);

                targetPosition = ray.GetPoint(hit.distance);

                Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

                transform.rotation = targetRotation;

            }
        }

        if (this.moveByPath) {

            if (this.isInSamePosition(this.transform.position, this.pathNodes[this.pathNodes.Count - 1].transform.position)) {
                this.moveByPath = false;
                this.actualNode = 0;
                this.pathNodes = new List<Nodo>();
                this._player.State = CharacterState.IDLE;
                //this._animator.SetInteger("Action", this._player.State.GetHashCode());
                return;
            } else if (this.actualNode < this.pathNodes.Count && this.isInSamePosition(this.transform.position, this.pathNodes[this.actualNode].transform.position)) {
                this._player.State = CharacterState.WALKING;
                this.actualNode++;
            }

            targetPosition = this.pathNodes[this.actualNode].transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);

            transform.rotation = targetRotation;
        }

        Vector3 dir = targetPosition - transform.position;

        float move = speed * Time.deltaTime;

        _controller.Move((targetPosition - transform.position).normalized * speed * Time.deltaTime);

        // Fix the rotation bug.
        if (this._fixedRotateZ != this.transform.rotation.z || this._fixedRotateX != this.transform.rotation.x) {
            this.transform.rotation = new Quaternion(this._fixedRotateX, this.transform.rotation.y, this._fixedRotateZ, this.transform.rotation.w);
        }

        if (this.isInSamePosition(this.transform.position, targetPosition) && this._player.State != CharacterState.CASTING) {
            this._player.State = CharacterState.IDLE;
            //this._animator.SetInteger("Action", this._player.State.GetHashCode());
        }
    }

    private void SelectArea() {
        if (Input.GetButtonDown("SelectArea")) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDist = Vector3.Distance(Input.mousePosition, Camera.main.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, hitDist) && hit.collider.gameObject.tag == "HermesShop") {
                float distance = Vector3.Distance(this.transform.position, hit.collider.transform.position);
                if (distance < 10) {
                    HermesShop hShop = hit.collider.GetComponent<HermesShop>();
                    hShop.AddPlayerOnQueue(this);
                    hShop.ShowShop(this);
                }
            }
        }
    }

    /// <summary>
    /// Verifica se os dois Vector3 estão na mesma posição.
    /// </summary>
    /// <param name="actualPosition"></param>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    private bool isInSamePosition(Vector3 actualPosition, Vector3 targetPosition) {
        if (Mathf.RoundToInt(actualPosition.x) == Mathf.RoundToInt(targetPosition.x) && Mathf.RoundToInt(actualPosition.z) == Mathf.RoundToInt(targetPosition.z))
            return true;

        return false;
    }


    /// <summary>
    /// Faz a movimentação seguindo o melhor caminho.
    /// Algoritmo utilizado é o AStar
    /// </summary>
    /// <param name="hitPosition"></param>
    private void MovePath(Vector3 hitPosition) {
        this.moveByPath = true;
        Nodo nodeOrigin = this.GetComponent<Nodo>();
        nodeOrigin.ListaAdj.Clear();

        List<GameObject> nodesDestino = new List<GameObject>(GameObject.FindGameObjectsWithTag("NodoDestino"));

        nodesDestino.ForEach(linq => { GameObject.Destroy(linq); });

        Instantiate(this.nodoDestino, hitPosition, Quaternion.identity);
        this.nodoDestino.transform.position = hitPosition;

        List<GameObject> nodesAdj = new List<GameObject>(GameObject.FindGameObjectsWithTag("Nodo"));

        if (nodesAdj.Count > 0) {
            nodesAdj.ForEach(linq => {
                linq.GetComponent<Nodo>().ListaAdj.Remove(this.nodoDestino);
                linq.GetComponent<Nodo>().adjacentes = linq.GetComponent<Nodo>().ListaAdj.ToArray();
            });

            List<GameObject> tst = nodesAdj.Where(l => l.GetComponent<Nodo>().adjacentes.Contains(nodoDestino)).ToList();
            // TODO: Verificar fazer adjacencia por um determinado Raio de circunferência.
            List<GameObject> originAdj = nodesAdj.Where(linq => Vector3.Distance(nodeOrigin.transform.position, linq.transform.position) < nodeDistance).ToList();

            foreach (GameObject n in originAdj) {
                nodeOrigin.ListaAdj = new List<Nodo>();
                Nodo node = n.GetComponent<Nodo>();
                nodeOrigin.ListaAdj.Add(node);
            }
            nodeOrigin.adjacentes = nodeOrigin.ListaAdj.ToArray();

            nodesAdj.ForEach(linq => {
                Nodo node = linq.GetComponent<Nodo>();

                node.G = Vector3.Distance(this.transform.position, linq.transform.position) + Vector3.Distance(linq.transform.position, hitPosition);
                node.H = Vector3.Distance(linq.transform.position, hitPosition);
            });
            List<GameObject> adj = nodesAdj.Where(linq => Vector3.Distance(hitPosition, linq.transform.position) < nodeDistance).ToList();
            foreach (GameObject item in adj) {
                Nodo node = item.GetComponent<Nodo>();
                node.ListaAdj.Add(this.nodoDestino);
                node.adjacentes = node.ListaAdj.ToArray();
            }
        }

        GameExecution exc = new GameExecution();
        exc.criaNodos(nodeOrigin, this.nodoDestino);

        pathNodes = exc.buscaAStar(nodeOrigin, this.nodoDestino);

        pathNodes.Remove(this.GetComponent<Nodo>());

    }



}
