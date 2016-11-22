using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GridSubTileData)), CanEditMultipleObjects]
public class SubTileDataEditor : Editor
{
    private SerializedProperty m_state;
    private SerializedProperty m_descriptionImage;
    private SerializedProperty m_videoPath;
    private SerializedProperty m_image;

    void OnEnable()
    {
        m_state = serializedObject.FindProperty("m_contentType");
        m_descriptionImage = serializedObject.FindProperty("m_descriptionImage");
        m_videoPath = serializedObject.FindProperty("m_videoPath");
        m_image = serializedObject.FindProperty("m_image");
    }
	
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(m_descriptionImage);

        EditorGUILayout.PropertyField(m_state);
        GridSubTileData.GridContentType contentType = (GridSubTileData.GridContentType)m_state.enumValueIndex;
        switch(contentType)
        {
            case GridSubTileData.GridContentType.Image:
                EditorGUILayout.PropertyField(m_image);
                break;
            case GridSubTileData.GridContentType.Video:
                EditorGUILayout.PropertyField(m_videoPath);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
