#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;


public class ObjectDuplicator : EditorWindow
{
    private GameObject selectedObject;
    private Vector3 duplicateOffset;
    private int numberOfDuplicates = 1;
    private GameObject[] duplicatedObjects;

    [MenuItem("Custom/Duplicate Object")]
    static void CreateWindow()
    {
        GetWindow<ObjectDuplicator>("Object Duplicator");
    }

    private void OnGUI()
    {
        selectedObject = Selection.activeGameObject;

        EditorGUILayout.LabelField("Object Duplicator");

        if (selectedObject != null)
        {
            EditorGUILayout.LabelField("Selected Object: " + selectedObject.name);

            duplicateOffset = EditorGUILayout.Vector3Field("Duplicate Offset", duplicateOffset);
            numberOfDuplicates = EditorGUILayout.IntField("Number of Duplicates", numberOfDuplicates);

            GUILayout.Space(10);

            if (GUILayout.Button("Duplicate"))
            {
                DuplicateObjects();
            }

            if (GUILayout.Button("Delete Duplicates"))
            {
                DeleteDuplicates();
            }
        }
        else
        {
            EditorGUILayout.LabelField("Select an object to duplicate.");
        }
    }

    private void DuplicateObjects()
    {
        duplicatedObjects = new GameObject[numberOfDuplicates];

        if (selectedObject != null)
        {
            for (int i = 0; i < numberOfDuplicates; i++)
            {
                Vector3 position = selectedObject.transform.position + (duplicateOffset * (i+1));
                GameObject newObject = (GameObject)PrefabUtility.InstantiatePrefab(PrefabUtility.GetCorrespondingObjectFromSource(selectedObject));
                newObject.transform.position = position;
                duplicatedObjects[i] = newObject;
            }
        }
    }

    private void DeleteDuplicates()
    {
        if (duplicatedObjects != null)
        {
            for (int i = 0; i < duplicatedObjects.Length; i++)
            {
                if (duplicatedObjects[i] != null)
                {
                    DestroyImmediate(duplicatedObjects[i]);
                }
            }
        }
    }

}
#endif
