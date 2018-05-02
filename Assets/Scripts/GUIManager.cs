﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System;

public class GUIManager : MonoBehaviour
{
    public MovementModel model;
    PIDRegulationController controller;
    public NavigationController navController;

    public InputField AbsMLimit;
    public InputField AbsRLimit;
    public InputField PCoefficient;
    public InputField ICoefficient;
    public InputField DCoefficient;

    public Dropdown PlaneRestrict;

    public Slider SimulationScale;
    public Text SimulatonScaleText;

    public InputField PointsSpawnRadius;
    public InputField PointsSpawnCount;

    public Text[] ControlLabels;
    public Text[] ErrorLabels;
    public bool updateValues = true;

    private void Start()
    {
        if (model == null)
            return;
        controller = model.GetComponent<PIDRegulationController>();
        model.OnMovementUpdate += UpdateGUI;
        AbsMLimit.text = model.MuLimits.x.ToString();
        AbsMLimit.onValueChanged.AddListener(delegate (string str)
        {
            float value = Convert.ToSingle(str);
            model.MuLimits.Set(value, value, value);
        });
        AbsRLimit.text = model.RuLimits.x.ToString();
        AbsRLimit.onValueChanged.AddListener(delegate (string str)
        {
            float value = Convert.ToSingle(str);
            model.RuLimits.Set(value, value, value);
        });
        PCoefficient.text = controller.P.ToString();
        PCoefficient.onValueChanged.AddListener(delegate (string str)
        {
            float value = Convert.ToSingle(str);
            controller.P = value;
        });
        ICoefficient.text = controller.I.ToString();
        ICoefficient.onValueChanged.AddListener(delegate (string str)
        {
            float value = Convert.ToSingle(str);
            controller.I = value;
        });
        DCoefficient.text = controller.D.ToString();
        DCoefficient.onValueChanged.AddListener(delegate (string str)
        {
            float value = Convert.ToSingle(str);
            controller.D = value;
        });
        DCoefficient.text = controller.D.ToString();
        PlaneRestrict.onValueChanged.AddListener(delegate (int index)
        {
            var value = (PIDRegulationController.PlaneRestrictionType)
            Enum.ToObject(typeof(PIDRegulationController.PlaneRestrictionType), index);
            controller.RestrictPlane = value;
        });
        PlaneRestrict.onValueChanged.Invoke(PlaneRestrict.value);
        SimulationScale.onValueChanged.AddListener(delegate (float value)
        {
            Time.timeScale = value;
            SimulatonScaleText.text = value.ToString();
        });
        PointsSpawnRadius.text = navController.PointsGenerationCubeSide.ToString();
        PointsSpawnRadius.onValueChanged.AddListener(delegate (string str)
        {
            float value = Convert.ToSingle(str);
            navController.PointsGenerationCubeSide = value;
        });
        PointsSpawnCount.text = navController.GeneratedPointsCount.ToString();
        PointsSpawnCount.onValueChanged.AddListener(delegate (string str)
        {
            int value = Convert.ToInt32(str);
            navController.GeneratedPointsCount = value;
        });
    }

    private void UpdateGUI()
    {
        ControlLabels[0].text = model.MuX.ToString();
        ControlLabels[1].text = model.MuY.ToString();
        ControlLabels[2].text = model.MuZ.ToString();
        ControlLabels[3].text = model.RuX.ToString();
        ControlLabels[4].text = model.RuY.ToString();
        ControlLabels[5].text = model.RuZ.ToString();

        ErrorLabels[0].text = controller.posError.x.ToString();
        ErrorLabels[1].text = controller.posError.y.ToString();
        ErrorLabels[2].text = controller.posError.z.ToString();
        ErrorLabels[3].text = controller.rotError.x.ToString();
        ErrorLabels[4].text = controller.rotError.y.ToString();
        ErrorLabels[5].text = controller.rotError.z.ToString();
    }
}
