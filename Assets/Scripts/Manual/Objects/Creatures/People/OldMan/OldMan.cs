using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldMan : MonoBehaviour
{
    public float OldManUltiDamage;
    public Transform Mama;
    public Transform Harm;
    public Sprite[] HeadSprites;
    Transform Head;
    float Dist;
    float Sign;
    float SignY;
    float MinY;
    float DistY;
    bool AttackMode;
    public float Speed;
    public int EM;
    public int[] Emotion;
    public string[] EmotionalSupport;
    bool UnEmoble;
    public bool HandCatched;
    bool Life = true;
    bool Killed;
    public float Health;
    public float MaxHealth;
    public AudioClip[] SoundEffects;
    public AudioSource SoundEffecter;
    Color FirstColor;
    bool Dream;
    bool DreamWait;
    bool OneDie;
    public bool Work;
    bool WaitMF;
    void Start()
    {
        Head = Mama.GetChild(Mama.childCount - 1);
        FirstColor = GetComponent<SpriteRenderer>().color;
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Work && GetIntForPlayer() != 5)
        {
            if (WaitMF)
            {
                if (!Killed)
                {
                    if (AttackMode) ChainMaker();
                    Mama.gameObject.SetActive(AttackMode);
                    if (!UnEmoble) StartCoroutine(EmoGirl());
                    if (Life) GameObject.FindGameObjectWithTag("Boss").GetComponent<Scrollbar>().size = Health / MaxHealth;
                    SoundEffecter.pitch = Random.Range(0.75f, 1.25f);
                    GameObject Player = GameObject.FindGameObjectWithTag("Player");
                    if (!DreamWait && !HandCatched && !AttackMode && Vector3.Distance(Player.transform.position, transform.position) < 20) StartCoroutine(GetDream());
                }
                else if (!OneDie)
                {
                    OneDie = true;
                    FindObjectOfType<Mission0>().KilledOldMan();
                }
                if (Health <= 0) Killed = true;
            }
            else
            {
                StartCoroutine(WaitMFFunc());
            }
        }
        else
        {
            Mama.gameObject.SetActive(false);
        }
    }
    IEnumerator WaitMFFunc()
    {
        yield return new WaitForSeconds(5);
        WaitMF = true;
    }
    IEnumerator GetDream()
    {
        if (!AttackMode)
        {
            DreamWait = true;
            yield return new WaitForSeconds(Random.Range(90f, 100f));
            Dream = true;
            AttackMode = false;
            Mama.gameObject.SetActive(false);
            FindObjectOfType<InteractManager>().NoMove();
            GameObject.FindGameObjectWithTag("Msiz").transform.localPosition = Vector3.zero;
            AudioSource NewEffecter = Instantiate(SoundEffecter, null);
            NewEffecter.GetComponent<Damager>().EffectTime = SoundEffects[7].length;
            NewEffecter.clip = SoundEffects[7];
            NewEffecter.gameObject.SetActive(true);
            float Passed = 0;
            while (Passed < 5)
            {
                SetIntForPlayer(4);
                yield return new WaitForSeconds(0.025f);
                Passed += 0.025f;
            }
            GameObject.FindGameObjectWithTag("Msiz").transform.localPosition = Vector3.one;
            yield return new WaitForSeconds(0.5f);
            FindObjectOfType<InteractManager>().AllOpened();
            Dream = false;
            DreamWait = false;
        }
    }
    IEnumerator EmoGirl()
    {
        UnEmoble = true;
        string Mode = EmotionalSupport[Emotion[EM]];
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < Mode.Length; i++)
            {
                GetEmotional(int.Parse(Mode[i].ToString()));
                if (int.Parse(Mode[i].ToString()) < 2) yield return new WaitForSeconds(3);
                else if (int.Parse(Mode[i].ToString()) == 3) yield return new WaitForSeconds(20);
                else yield return new WaitForSeconds(1);
                yield return new WaitUntil(() => FindObjectOfType<GratiasMovement>().enabled && !Dream && !Killed);
                yield return new WaitForSeconds(1);
            }
        }
        EM ++;
        if (EM >= Emotion.Length) EM = 0;
        UnEmoble = false;
    }
    void GetEmotional(int Type)
    {
        if (Type == 0)
        {
            AudioSource NewEffecter = Instantiate(SoundEffecter, null);
            NewEffecter.GetComponent<Damager>().EffectTime = SoundEffects[0].length;
            NewEffecter.clip = SoundEffects[0];
            NewEffecter.gameObject.SetActive(true);
            StartCoroutine(MovePlayer());
        }
        else if (Type == 1)
        {
            AudioSource NewEffecter = Instantiate(SoundEffecter, null);
            NewEffecter.GetComponent<Damager>().EffectTime = SoundEffects[1].length;
            NewEffecter.clip = SoundEffects[1];
            NewEffecter.gameObject.SetActive(true);
            StartCoroutine(DashPlayer());
        }
        else if (Type == 2)
        {
            StartCoroutine(IdlePlayer());
        }
        else if (Type == 3)
        {
            AudioSource NewEffecter = Instantiate(SoundEffecter, null);
            NewEffecter.GetComponent<Damager>().EffectTime = SoundEffects[3].length;
            NewEffecter.clip = SoundEffects[3];
            NewEffecter.gameObject.SetActive(true);
            StartCoroutine(AttackPlayer());
        }
    }
    IEnumerator MovePlayer()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        int Way = (int)Mathf.Sign(Player.transform.position.x - transform.position.x);
        GetComponent<SpriteRenderer>().flipX = Transformation.FloatToBoolian(Way);
        float Passed = 0;
        while (Vector3.Distance(transform.position, Player.transform.position)>5f)
        {
            yield return new WaitForSeconds(0.025f);
            Passed += 0.025f;
            if (GetIntForPlayer()!=5) SetIntForPlayer(1);
            transform.position=Vector3.Lerp(transform.position, new Vector3(Player.transform.position.x, transform.position.y), Mathf.Clamp(Speed / Vector3.Distance(transform.position, Player.transform.position), 0.001f, 1));
            if (Way != Mathf.Sign(Player.transform.position.x - transform.position.x))
            {
                break;
            }
            if (Passed >= 2) break;
            if (Dream || Killed) break;
        }
        Vector3 Dest=transform.position + Vector3.right * 10 * Way;
        yield return new WaitForSeconds(0.5f);
        if (Way != Mathf.Sign(Player.transform.position.x - transform.position.x))
        {
            Passed = 0;
            while (Vector3.Distance(transform.position, Dest) > 1f)
            {
                yield return new WaitForSeconds(0.025f);
                Passed += 0.025f;
                transform.position = Vector3.Lerp(transform.position, Dest, Mathf.Clamp(Speed / Vector3.Distance(transform.position , Dest) * 2, 0.001f, 1));
                if (Passed >= 1) break;
                if (Dream || Killed) break;
            }
        }
        SetIntForPlayer(0);
    }
    IEnumerator DashPlayer()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        int Way = (int)Mathf.Sign(Player.transform.position.x - transform.position.x);
        GetComponent<SpriteRenderer>().flipX = Transformation.FloatToBoolian(Way);
        Vector3 DashPoint = new Vector3(Player.transform.position.x,transform.position.y) + Way * 10 * Vector3.right;
        GetComponent<Collider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        if (GetIntForPlayer() != 5) SetIntForPlayer(3);
        yield return new WaitForSeconds(0.875f);
        float Passed = 0;
        while (Vector3.Distance(transform.position, DashPoint) > 5f)
        {
            GameObject Harmy = Instantiate(Harm, Harm.transform.position, Quaternion.identity).gameObject;
            Harmy.SetActive(true);
            yield return new WaitForSeconds(0.025f);
            Passed += 0.025f;
            if (GetIntForPlayer() != 5) SetIntForPlayer(3);
            transform.position = Vector3.Lerp(transform.position, DashPoint, Mathf.Clamp(Speed / Vector3.Distance(transform.position, DashPoint) * 7.5f, 0.001f, 1));
            if (Passed >= 2.125f) break;
            if (Dream || Killed) break;
        }
        GetComponent<Collider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        SetIntForPlayer(0);
    }
    IEnumerator IdlePlayer()
    {
        SetIntForPlayer(0);
        yield return new WaitForSeconds(1f);
    }
    IEnumerator AttackPlayer()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        int Way = (int)Mathf.Sign(Player.transform.position.x - transform.position.x);
        GetComponent<SpriteRenderer>().flipX = Transformation.FloatToBoolian(Way);
        SetIntForPlayer(2);
        Head.transform.position = transform.position + Vector3.up * 3;
        StartCoroutine(HandAttack(transform.position + (Random.Range(0, 2) * 2 - 1) * Vector3.right * 15, 7.5f, false));
        AttackMode = true;
        float Passed = 0;
        while (Passed <= 20f)
        {
            yield return new WaitForSeconds(0.025f);
            if (GetIntForPlayer() != 5) SetIntForPlayer(2);
            Passed += 0.025f;
            if (Passed % 2 < 0.025f && Passed >= 3)
            {
                if (Mathf.Abs(Passed - 2) > 0.025f && Mathf.Abs(Passed - 6) > 0.025f && Mathf.Abs(Passed - 8) > 0.025f && Mathf.Abs(Passed - 12) > 0.025f && Mathf.Abs(Passed - 16) > 0.025f && Mathf.Abs(Passed - 18) > 0.025f) StartCoroutine(HandAttack(Player.transform.position, 7.5f, true));
                else StartCoroutine(HandAttack(Player.transform.position, 0, false));
            }
            else Head.transform.position = Vector3.Lerp(Head.transform.position, transform.position + Vector3.up * 5 + Vector3.right * 15, Mathf.Clamp(Speed / Vector3.Distance(Head.transform.position, transform.position + Vector3.up * 5 + Vector3.right * 15) * 2f, 0.001f, 1));
            if (!FindObjectOfType<GratiasMovement>().enabled) break;
            if (Dream || Killed) break;
        }
        yield return new WaitUntil(() => FindObjectOfType<GratiasMovement>().enabled);
        AttackMode = false;
        SetIntForPlayer(0);
    }

    IEnumerator HandAttack(Vector3 Pos, float Dist, bool WaitDown)
    {
        AudioSource NewEffecter = Instantiate(SoundEffecter, null);
        NewEffecter.GetComponent<Damager>().EffectTime = SoundEffects[4].length;
        NewEffecter.clip = SoundEffects[4];
        NewEffecter.gameObject.SetActive(true);
        Vector3 diffR = Head.position - Pos;
        diffR.Normalize();
        float rot_zs = Mathf.Atan2(diffR.y, diffR.x) * Mathf.Rad2Deg;
        Head.rotation = Quaternion.Euler(0f, 0f, rot_zs);
        Vector3 PosFixed = Pos.x * Vector3.right - Vector3.up * 4;
        while (Vector3.Distance(Head.transform.position, PosFixed) > 1)
        {
            yield return new WaitForSeconds(0.01f);
            Head.transform.position = Vector3.Lerp(Head.transform.position, PosFixed, Mathf.Clamp(Speed / Vector3.Distance(Head.transform.position, PosFixed) * 3.5f, 0.001f, 1));
            if (Dream || Killed) break;
        }
        while (Vector3.Distance(Head.transform.position, PosFixed + Vector3.up * 5) > 1)
        {
            yield return new WaitForSeconds(0.01f);
            Head.transform.position = Vector3.Lerp(Head.transform.position, PosFixed + Vector3.up * 5, Mathf.Clamp(Speed / Vector3.Distance(Head.transform.position, PosFixed + Vector3.up * 5) * 4.5f, 0.001f, 1));
            if (Dream || Killed) break;
        }
        while (Vector3.Distance(Head.transform.position, Pos + Vector3.up * Dist) > 1)
        {
            yield return new WaitForSeconds(0.01f);
            Head.transform.position = Vector3.Lerp(Head.transform.position, Pos + Vector3.up * Dist, Mathf.Clamp(Speed / Vector3.Distance(Head.transform.position, Pos + Vector3.up * Dist) * 3.5f, 0.001f, 1));
            if (HandCatched)
            {
                StartCoroutine(HitPlayer());
                break;
            }
            if (Dream || Killed) break;
        }
        if (WaitDown) yield return new WaitForSeconds(0.75f);
        while (Vector3.Distance(Head.transform.position, PosFixed) > 1)
        {
            yield return new WaitForSeconds(0.01f);
            Head.transform.position = Vector3.Lerp(Head.transform.position, PosFixed, Mathf.Clamp(Speed / Vector3.Distance(Head.transform.position, PosFixed) * 4.5f, 0.001f, 1));
            if (HandCatched)
            {
                StartCoroutine(HitPlayer());
                break;
            }
            if (Dream || Killed) break;
        }
        yield return new WaitUntil(() => FindObjectOfType<GratiasMovement>().enabled);
        HandCatched = false;
    }
    IEnumerator HitPlayer()
    {
        FindObjectOfType<CameraManager>().CrackTheCamera(10, 0.225f, 7.5f);
        Debug.Log("Damaged");
        GameObject Player=GameObject.FindGameObjectWithTag("Player");
        FindObjectOfType<InteractManager>().NoMove();
        int Way = (int)Mathf.Sign(Player.transform.position.x - transform.position.x);
        Head.position = Player.transform.position + Vector3.right;
        Head.GetChild(0).GetComponent<SpriteRenderer>().sprite = HeadSprites[1];
        Head.GetComponent<Collider2D>().isTrigger = true;
        float Passed = 0;
        while (Vector3.Distance(Head.transform.position, transform.position + Way * Vector3.right * 20) > 1)
        {
            yield return new WaitForSeconds(0.01f);
            Head.transform.position = Vector3.Lerp(Head.transform.position, transform.position + Way * Vector3.right * 20, Mathf.Clamp(Speed / Vector3.Distance(Head.transform.position, transform.position + Way * Vector3.right * 20) * 7.5f, 0.001f, 1));
        }
        float angle = 0;
        float radius = 20;
        bool Equalizer = !Transformation.FloatToBoolian(Way);
        if (Equalizer) Passed = 0;
        else Passed = 7.5f;
        for (int i = 0; i < 6; i++)
        {
            if (Equalizer) while (Passed < 7.5f)
                {
                    Passed += 0.2f;
                    angle = 7.5f - Passed;
                    yield return new WaitForSeconds(0.01f);
                    Head.position = transform.position + new Vector3(Mathf.Cos(angle / 7.5f * Mathf.PI), Mathf.Sin(angle / 7.5f * Mathf.PI)) * radius;
                    radius -= 0.04f;
                }
            else while (Passed > 0)
                {
                    Passed -= 0.2f;
                    angle = 7.5f - Passed;
                    yield return new WaitForSeconds(0.01f);
                    Head.position = transform.position + new Vector3(Mathf.Cos(angle / 7.5f * Mathf.PI), Mathf.Sin(angle / 7.5f * Mathf.PI)) * radius;
                    radius -= 0.04f;
                }
            AudioSource NewEffecter = Instantiate(SoundEffecter, null);
            NewEffecter.GetComponent<Damager>().EffectTime = SoundEffects[5].length;
            NewEffecter.clip = SoundEffects[5];
            NewEffecter.gameObject.SetActive(true);
            Equalizer = !Equalizer;
            FindObjectOfType<HealthManagment>().DecreaseHealth(OldManUltiDamage);
            yield return new WaitForSeconds(0.1f);
        }
        Head.GetChild(0).GetComponent<SpriteRenderer>().sprite = HeadSprites[0];
        Head.GetComponent<Collider2D>().isTrigger = false;
        FindObjectOfType<InteractManager>().AllOpened();
    }
    void SetIntForPlayer(int Int, string Name) //OldManAnim
    {
        GetComponent<Animator>().SetInteger(Name, Int);
    }
    void SetIntForPlayer(int Int) //OldManAnim
    {
        GetComponent<Animator>().SetInteger("Act", Int);
    }
    int GetIntForPlayer(int Int, string Name) //OldManAnim
    {
        return GetComponent<Animator>().GetInteger(Name);
    }
    int GetIntForPlayer() //OldManAnim
    {
        return GetComponent<Animator>().GetInteger("Act");
    }
    void ChainMaker()
    {
        Dist = Mathf.Clamp(Mathf.Abs(Vein(-1).position.x - Head.position.x), 0.1f, Mathf.Infinity);
        Sign = Mathf.Sign(Head.position.x - Vein(-1).position.x);
        SignY = Mathf.Sign(Head.position.y - Vein(-1).position.y);
        MinY = (SignY + 1) / 2 * Vein(-1).position.y + (-SignY + 1) / 2 * Head.position.y;
        DistY = Mathf.Clamp(Mathf.Abs(Head.position.y - Vein(-1).position.y), 0.1f, Mathf.Infinity); ;
        Vector3 PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        int Way = 0;
        if (GetComponent<SpriteRenderer>().flipX) Way = 1;
        else Way = -1;
        Vein(-1).localPosition = Mathf.Abs(Vein(-1).localPosition.x) * Way * Vector3.right + Vector3.up;
        for (int i = 0; i < Mama.childCount - 2; i++)
        {
            Vein(i).position = new Vector3(Head.position.x - Dist / (Mama.childCount - 2) * Sign * (i + 1), Vein(i).position.y);
            float Vy = Mathf.Clamp(Mathf.Clamp((1f / 2f) * Dist * Mathf.Sin(Mathf.Abs(Vein(i).position.x - Vein(-1).position.x) / Dist * Mathf.PI * 4) * DistY / 25, -Dist, Dist) + Head.position.y * Mathf.Clamp(Mama.childCount / 2 - 1 - i, 0, Mathf.Infinity) / ((float)Mama.childCount / 2 - 1) + Vein(-1).position.y * Mathf.Clamp(-Mama.childCount / 2 - 1 + i, 0, Mathf.Infinity) / ((float)Mama.childCount / 2f - 1), PlayerPos.y - 0.75f, 45);
            Vein(i).position = new Vector3(Vein(i).position.x, Vy);

            if (i != 0)
            {
                Vector3 diff = Vein(i - 1).position - Vein(i).position;
                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                Vein(i).rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                Vein(i).localScale = Vector3.one / 2;
            }
            else
            {
                Vector3 diff = Head.position - Vein(i).position;
                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                Vein(i).rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                Vein(i).localScale = Vector3.one / 2;
            }
        }
    }
    Transform Vein(int Value)
    {
        return Mama.GetChild(Value + 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Damager>(out Damager New))
        {
            if (Life)
            {
                if (!FindObjectOfType<GratiasMovement>().GodMode)
                {
                    if (!AttackMode) Health -= New.Power;
                    else Health -= New.Power * 10;
                }
                else Health = 0;
                StartCoroutine(Damaged());
            }
            else
            {
                Killed = true;
            }
        }
    }
    IEnumerator Damaged()
    {
        AudioSource NewEffecter = Instantiate(SoundEffecter, null);
        NewEffecter.GetComponent<Damager>().EffectTime = SoundEffects[6].length;
        NewEffecter.clip = SoundEffects[6];
        NewEffecter.gameObject.SetActive(true);
        SpriteRenderer MyMe = GetComponent<SpriteRenderer>();
        for (int i = 0; i < 7; i++)
        {
            MyMe.color = Color.white - MyMe.color + Color.black;
            yield return new WaitForSeconds(0.1f);
        }
        MyMe.color = FirstColor;
    }
}
