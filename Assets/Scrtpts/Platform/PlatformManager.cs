using System;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    private int count;
    private int positionX;

    public GameObject [] scaffold;
    [SerializeField] int platformCount = 20;

    private Keyboard keyBoard;

    private void Awake()
    {
        keyBoard = new Keyboard();
        scaffold = new GameObject[20];
    }

    void Start()
    {
        CreatePlatform(platformCount);    
    }

    public int RandomPositionX()
    {
        if(UnityEngine.Random.Range(0, 2) == 0)
            return positionX += 1;         
        else
            return positionX -= 1; 
    }

    public void CreatePlatform(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject temporary = Instantiate
            (
                Resources.Load<GameObject>("Scaffold"),
                new Vector3 ( RandomPositionX(), i / 2f, 0),
                Quaternion.identity
            );
            
            temporary.transform.SetParent(transform);
            scaffold[i] = temporary;
        }
    }

    public void Position(Transform newPosition)
    {
        newPosition.transform.position = new Vector3
        (
             RandomPositionX() + transform.position.x,
             newPosition.transform.position.y + 10f,
             newPosition.transform.position.z
        );
    }

    public void StepUpButton()
    {
        keyBoard.StairKey(transform);

        // Platform Manager ������Ʈ�� ��ġ - scaffold[�ε���]�� ���� ��ġ
        if (transform.position.x + scaffold[count].transform.localPosition.x != 0)
        {     
            GameManager.Instance.StateCanvas(GameManager.state.Exit);
        }
   
        Position(scaffold[count].transform);

        count++;

        if(count == platformCount) count = 0;    
    }

    public void DirectionButton()
    {
        keyBoard.TurnKey();

        StepUpButton();
    }
}
