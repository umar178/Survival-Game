using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInstantiation : MonoBehaviour
{
    [SerializeField] GameObject treePrefab;
    [SerializeField] Transform treeTransform;
    public Terrain terrain;


    public int numberOfTrees = 100;
    public float treeXSpacing = 5.0f;
    public float treeZSpacing = 5.0f;
    [SerializeField] float TreeDamping = 0;
    [SerializeField] int limitHeight;
    [SerializeField] float waterLevel;

    [SerializeField] float Childcount;
    void Start()
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            float x = Random.Range(treeTransform.position.x - treeXSpacing, treeTransform.position.x + treeXSpacing);
            float z = Random.Range(treeTransform.position.z - treeZSpacing, treeTransform.position.z + treeZSpacing);
            Vector3 treePosition = new Vector3(x, 0, z);
            float y = terrain.SampleHeight(treePosition);
            treePosition.y = y - TreeDamping;
            if (treePosition.y > 200) { return; }
            GameObject tree = Instantiate(treePrefab, treePosition, Quaternion.identity);
            tree.transform.parent = treeTransform;
        }
        Invoke("DelTrees", 0.1f);
    }

    private void DelTrees()
    {
        /*Health[] trees = GameObject.FindObjectsOfType<Health>();
        Debug.Log(trees.Length);

        foreach (Health tree in trees)
        {
            if(tree.tag == "Tree")
            {
                if(tree.transform.position.y > limitHeight)
                {
                    Destroy(tree.gameObject);
                }
                
                if(tree.transform.position.y < waterLevel)
                {
                    Destroy(tree.gameObject);
                }

            }
        }*/
        
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).tag == "Tree")
            {
                if(transform.GetChild(i).transform.position.y > limitHeight)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
                
                if(transform.GetChild(i).transform.position.y < waterLevel)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }

            }
        }
        Debug.Log("next 1");
    }

    private void Update()
    {
        Childcount = transform.childCount;
    }
}
