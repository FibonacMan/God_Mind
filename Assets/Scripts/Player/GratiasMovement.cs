using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GratiasMovement : MonoBehaviour
{
    float Speed;
    public GameObject SpeedObject;
    public float JumpHeight;
    Transform Player;
    bool JumpTrue = true;
    public bool DashTrue = false;
    public bool AttackTrue = false;
    public bool GodMode = false;
    public float DashDistance;
    float time1D;
    float time2D;
    bool isTapD = false;
    float time1A;
    float time2A;
    bool isTapA = false;
    public bool NoMove = false;
    public Transform AttackSprites;
    public Vector3[] AttackPositions;
    public Vector3[] AttackRotation;
    public Color ToolColor;
    public GameObject[] AttackPoints;
    bool Attackable = true;
    public Toggle GodMindToggle;
    void Update()
    {
        if(GodMindToggle.gameObject.activeSelf) GodMode = GodMindToggle.isOn;
        else GodMode = false;




        //Naming
        if (!GodMode) Speed = SpeedObject.transform.localPosition.x;
        else Speed = SpeedObject.transform.localPosition.x * 10;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        //Naming

        if (GodMode) FindObjectOfType<HealthManagment>().FullHealth();

        if (Player.GetComponent<Rigidbody2D>().velocity.y < 0) //GravityUpgrader
        {
            Player.GetComponent<Rigidbody2D>().velocity -= Vector2.up * Time.deltaTime;
        }


        if (!NoMove)
        {

            if (Input.GetKeyDown(KeyCode.Q) && AttackTrue && FindObjectOfType<EnergyManagment>().IsEnergyEnough() && Attackable)
            {
                StartCoroutine(Attack());
            }



            Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; //GravityUpgraderAndRotationFreezerWhenMovementStart
            if (Input.GetKey(KeyCode.W) && JumpTrue && !GodMode) StartCoroutine(Jump()); //JumpActivator

            if (Input.GetKey(KeyCode.A)) //ForA
            {
                Player.position -= Vector3.right * Speed * Time.deltaTime;
                Player.transform.localScale = Vector3.right * -2 + Vector3.up * 2;
            }
            if (Input.GetKey(KeyCode.D)) //ForD
            {
                Player.position += Vector3.right * Speed * Time.deltaTime;
                Player.transform.localScale = Vector3.one * 2;
            }
            if (GodMode)
            {
                Player.GetComponent<Rigidbody2D>().gravityScale = 0;
                if (Input.GetKey(KeyCode.W)) //ForA
                {
                    Player.position += Vector3.up * Speed * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.S)) //ForD
                {
                    Player.position -= Vector3.up * Speed * Time.deltaTime;
                }
            }
            else Player.GetComponent<Rigidbody2D>().gravityScale = 1;



            if (DashTrue)
            {
                if (Input.GetKeyDown(KeyCode.D)) // DashD
                {
                    if (isTapD == true)
                    {
                        time1D = Time.time;
                        isTapD = false;

                        if (time1D - time2D < 0.25f && FindObjectOfType<EnergyManagment>().IsEnergyEnough()) // interval between two clicked
                        {
                            StartCoroutine(Dash(1));
                        }
                    }
                }
                else // NoDashD
                {
                    if (isTapD == false)
                    {
                        time2D = Time.time;
                        isTapD = true;
                    }
                }
                if (Input.GetKeyDown(KeyCode.A)) // DashA
                {
                    if (isTapA == true)
                    {
                        time1A = Time.time;
                        isTapA = false;

                        if (time1A - time2A < 0.25f && FindObjectOfType<EnergyManagment>().IsEnergyEnough()) // interval between two clicked
                        {
                            StartCoroutine(Dash(-1));
                        }
                    }
                }
                else // NoDashA
                {
                    if (isTapA == false)
                    {
                        time2A = Time.time;
                        isTapA = true;
                    }
                }
            }



            //General
            if (Player.GetChild(Player.childCount - 1).localPosition.x == -1) //Standing
            {
                if (JumpTrue) //NoJump
                {
                    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) //Walking
                    {
                        SetIntForPlayer(1);
                        if (!FindObjectOfType<EffectManager>().GratiasEffects[2].isPlaying) FindObjectOfType<EffectManager>().GratiasEffects[2].pitch = Random.Range(0.75f,1.25f);
                        if (!FindObjectOfType<EffectManager>().GratiasEffects[2].isPlaying) FindObjectOfType<EffectManager>().GratiasEffects[2].Play();
                    }
                    else //NoWalking
                    {
                        FindObjectOfType<EffectManager>().GratiasEffects[2].Stop();
                        SetIntForPlayer(0);
                    }
                }
                else SetIntForPlayer(3); //Jump
            }
            //General
        }
        else SetIntForPlayer(4);



    }
    IEnumerator Dash(int Direction)
    {
        if (!GodMode) FindObjectOfType<EnergyManagment>().DecreaseEnergy(10);
        ParticleSystem DashParticle = Player.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        int[] Rotations = new int[] { 0, 180 };
        FindObjectOfType<EffectManager>().GratiasEffects[0].Play();
        NoMove = true;
        SetIntForPlayer(4);
        DashParticle.transform.localEulerAngles = Vector3.forward * Rotations[(int)((Mathf.Sign(Player.localScale.x) + 1) / 2)];
        DashParticle.Play();
        Player.GetComponent<Rigidbody2D>().isKinematic = true;
        FindObjectOfType<CameraManager>().CrackTheCamera(10, 0.05f, 5);
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.01f);
            Player.transform.position = Vector3.Lerp(Player.transform.position, Player.transform.position + Vector3.right * Direction * DashDistance + Vector3.up / 100, 0.1f);
        }
        Player.GetComponent<Rigidbody2D>().isKinematic = false;
        NoMove = false;
        DashParticle.Stop();
        SetIntForPlayer(0);
    }



    IEnumerator Dash(int Direction, float Distance)
    {
        if (!GodMode) FindObjectOfType<EnergyManagment>().DecreaseEnergy(10);
        ParticleSystem DashParticle = Player.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        int[] Rotations = new int[] { 0, 180 };
        FindObjectOfType<EffectManager>().GratiasEffects[0].Play();
        NoMove = true;
        SetIntForPlayer(4);
        DashParticle.transform.localEulerAngles = Vector3.forward * Rotations[(int)((Mathf.Sign(Player.localScale.x) + 1) / 2)];
        DashParticle.Play();
        Player.GetComponent<Rigidbody2D>().isKinematic = true;
        FindObjectOfType<CameraManager>().CrackTheCamera(10, 0.05f, 5);
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.01f);
            Player.transform.position = Vector3.Lerp(Player.transform.position, Player.transform.position + Vector3.right * Direction * DashDistance / Distance + Vector3.up / 100, 0.1f);
        }
        Player.GetComponent<Rigidbody2D>().isKinematic = false;
        NoMove = false;
        DashParticle.Stop();
        SetIntForPlayer(0);
    }



    IEnumerator MiniDash(int Direction, float Distance, float Time)
    {
        ParticleSystem DashParticle = Player.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        int[] Rotations = new int[] { 0, 180 };
        DashParticle.transform.localEulerAngles = Vector3.forward * Rotations[(int)((Mathf.Sign(Player.localScale.x) + 1) / 2)];
        DashParticle.Play();
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(Time / 10);
            Player.transform.position = Vector3.Lerp(Player.transform.position, Player.transform.position + Vector3.right * Direction * DashDistance / Distance + Vector3.up / 100, 0.1f);
        }
        DashParticle.Stop();
    }



    IEnumerator Attack()
    {
        NoMove = true;
        if(!GodMode) FindObjectOfType<EnergyManagment>().DecreaseEnergy(30);
        SetIntForPlayer(4);
        SetLayerForPlayer(1);
        SetIntForPlayer(1, "Four");
        FindObjectOfType<CameraManager>().CrackTheCamera(10, 0.05f, 3.5f, new Vector2(1, 0));
        FindObjectOfType<CameraManager>().ZoomTheCamera(0.2f, 0, 13.5f);
        Player.GetComponent<Rigidbody2D>().gravityScale=0.5f;
        FindObjectOfType<EffectManager>().GratiasEffects[1].Play();
        StartCoroutine(MiniDash((int)Mathf.Sign(Player.localScale.x), 7.5f, 0.25f));
        Player.GetChild(2).GetComponent<SpriteRenderer>().color = ToolColor;
        float WaitedTime = 0;
        bool OneTimed = true;
        bool[] Cloned = new bool[AttackPoints.Length];
        AttackPoints[0].transform.parent.localScale = new Vector3(Mathf.Sign(Player.transform.localScale.x), 1, 1);
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            WaitedTime += 0.025f;
            if (WaitedTime > 1f && OneTimed)
            {
                OneTimed = false;
                FindObjectOfType<CameraManager>().CrackTheCamera(10, 0.05f, 3.5f, new Vector2(1, 1), 0);
                StartCoroutine(MiniDash((int)Mathf.Sign(Player.localScale.x), 5.5f, 0.15f));
            }
            if (Transformation.FindSprite(AttackSprites.GetComponent<ImAsKeeper>().Assets, Player.GetComponent<SpriteRenderer>().sprite) >= AttackPositions.Length)
            {
                break;
            }
            else if (Transformation.CheckSprite(AttackSprites.GetComponent<ImAsKeeper>().Assets, Player.GetComponent<SpriteRenderer>().sprite))
            {
                if(!Cloned[Transformation.FindSprite(AttackSprites.GetComponent<ImAsKeeper>().Assets, Player.GetComponent<SpriteRenderer>().sprite)])
                {
                    Cloned[Transformation.FindSprite(AttackSprites.GetComponent<ImAsKeeper>().Assets, Player.GetComponent<SpriteRenderer>().sprite)] = true;
                    GameObject Point = AttackPoints[Transformation.FindSprite(AttackSprites.GetComponent<ImAsKeeper>().Assets, Player.GetComponent<SpriteRenderer>().sprite)];
                    GameObject Spawned = Instantiate(Point, Point.transform.position, Quaternion.identity);
                    Spawned.SetActive(true);
                }
                Player.GetChild(2).localPosition = AttackPositions[Transformation.FindSprite(AttackSprites.GetComponent<ImAsKeeper>().Assets, Player.GetComponent<SpriteRenderer>().sprite)];
                Player.GetChild(2).localEulerAngles = AttackRotation[Transformation.FindSprite(AttackSprites.GetComponent<ImAsKeeper>().Assets, Player.GetComponent<SpriteRenderer>().sprite)];
            }
        }
        Player.GetComponent<Rigidbody2D>().gravityScale = 1f;
        StartCoroutine(MiniDash((int)Mathf.Sign(Player.localScale.x), 7.5f, 0.1f));
        Player.GetChild(2).GetComponent<SpriteRenderer>().color = Color.clear;
        SetLayerForPlayer(0);
        SetIntForPlayer(0, "Four");
        SetIntForPlayer(0);
        StartCoroutine(WaitToAttackLitBit());
        NoMove = false;
    }


    IEnumerator WaitToAttackLitBit()
    {
        Attackable = false;
        yield return new WaitForSeconds(1);
        Attackable = true;
    }



    IEnumerator Jump() //Jump
    {
        JumpTrue = false;
        Player.GetComponent<Rigidbody2D>().velocity += Vector2.up * JumpHeight;
        yield return new WaitForSeconds(JumpHeight / 5f);
        JumpTrue = true;
    }



    public void SetIntForPlayer(int Int) //PlayerAnim
    {
        Player.GetComponent<Animator>().SetInteger("Act", Int);
    }



    public void SetIntForPlayer(int Int, string Name) //PlayerAnim
    {
        Player.GetComponent<Animator>().SetInteger(Name, Int);
    }



    public void SetLayerForPlayer(int Int) //PlayerAnim
    {
        for(int i=0;i< Player.GetComponent<Animator>().layerCount;i++) Player.GetComponent<Animator>().SetLayerWeight(i, 0);
        Player.GetComponent<Animator>().SetLayerWeight(Int, 1);
    }


    public int Layer()
    {
        int Lay = 0;
        for (int i = 1; i < Player.GetComponent<Animator>().layerCount; i++) if (Player.GetComponent<Animator>().GetLayerWeight(i) > 0) Lay = i;
        return Lay;
    }



    Transform GetPlayerPChild(int Int) //ObjectForControlSelecter
    {
        return Player.parent.GetChild(Int);
    }
}