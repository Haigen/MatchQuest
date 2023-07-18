using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(ArrayLayout))]
public class CustPropertyDrawer : PropertyDrawer {

    public override void OnGUI(Rect position,SerializedProperty property,GUIContent label){
        EditorGUI.PrefixLabel(position,label);
        Rect newposition = position;
        newposition.y += 18f;
        SerializedProperty data = property.FindPropertyRelative("rows");
        if (data.arraySize != 9)
            data.arraySize = 9;
        //data.rows[0][]
        for(int j=0;j<9;j++){
            SerializedProperty row = data.GetArrayElementAtIndex(j).FindPropertyRelative("row");
            newposition.height = 18f;
            if(row.arraySize != 9)
                row.arraySize = 9;
            newposition.width = 18f;
            for(int i=0;i<9;i++){
                EditorGUI.PropertyField(newposition,row.GetArrayElementAtIndex(i),GUIContent.none);
                newposition.x += newposition.width;
            }

            newposition.x = position.x;
            newposition.y += 18f;
        }

        /*string level = "";
        EditorGUILayout.TextField("Level", level);
        if(GUILayout.Button("Build Object"))
        {
            //output the board layout for using later
        }
        */
    }
    
    

    public override float GetPropertyHeight(SerializedProperty property,GUIContent label){
        return 18f * 12;
    }
}