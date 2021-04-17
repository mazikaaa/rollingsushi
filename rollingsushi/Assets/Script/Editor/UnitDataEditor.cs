using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;   

[CustomEditor(typeof(textdata))]
[CanEditMultipleObjects]
public class UnitDataEditor : Editor
{
    //ユニットデータを編集しやすくするため、エディターを拡張するために使用

    SerializedProperty nameProperty, likeProperty, leaveProperty, amountProperty, rateProperty, recastProperty, costProperty, eatProperty, skillProperty, skilldetailProperty;

    void OnEnable()
    {
        nameProperty = serializedObject.FindProperty("name");
        likeProperty = serializedObject.FindProperty("like");
        leaveProperty = serializedObject.FindProperty("leavetime");
        amountProperty = serializedObject.FindProperty("amount");
        rateProperty = serializedObject.FindProperty("rate");
        recastProperty = serializedObject.FindProperty("recast");
        costProperty = serializedObject.FindProperty("cost");
        eatProperty = serializedObject.FindProperty("eattime");
        skillProperty = serializedObject.FindProperty("skill");

        skilldetailProperty = serializedObject.FindProperty("skill_detail");

    }

    //エディターで編集したデータを保存する
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        nameProperty.stringValue = EditorGUILayout.TextField("名前", nameProperty.stringValue);
        likeProperty.stringValue = EditorGUILayout.TextField("好きな寿司", likeProperty.stringValue);
        leaveProperty.stringValue = EditorGUILayout.TextField("待機時間", leaveProperty.stringValue);
        amountProperty.stringValue = EditorGUILayout.TextField("食べる量", amountProperty.stringValue);
        rateProperty.stringValue = EditorGUILayout.TextField("確率", rateProperty.stringValue);
        recastProperty.stringValue = EditorGUILayout.TextField("食べる間隔", recastProperty.stringValue);
        costProperty.stringValue = EditorGUILayout.TextField("コスト", costProperty.stringValue);
        eatProperty.stringValue = EditorGUILayout.TextField("着席時間", eatProperty.stringValue);
        skillProperty.stringValue = EditorGUILayout.TextField("スキル", skillProperty.stringValue);
        EditorGUILayout.LabelField("スキルの詳細");
        skilldetailProperty.stringValue = EditorGUILayout.TextArea( skilldetailProperty.stringValue, GUILayout.Height(100));


        serializedObject.ApplyModifiedProperties();
    }
}

