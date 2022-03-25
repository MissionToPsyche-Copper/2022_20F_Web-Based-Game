using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DSOC_Questions : MonoBehaviour
{
    public struct Questions
    {
        public int id;
        public string question;
        public string answer;
    }

    public static Questions[] Level1Questions = {
        new Questions() {id = 1,
        question = "New Laser communication technology that encodes photons to communicate between a probe in deep space and earth.",
        answer = "Deep Space Optical Communication"} ,
        new Questions() {id = 2,
        question = "Provides high-resolution images using filters to discriminate between Psyche's metallic and silicate constituents.",
        answer = "MultiSpectral Imager"} ,
        new Questions() {id = 3,
        question = "Will detect, measure and map Psyche's elemental composition.",
        answer = "Gamma Ray and Neutron Spectrometer"} ,
        new Questions() {id = 4,
        question = "Designed to detect and measure the remanent magnetic field of the asteroid.",
        answer = "Magnetometer"} ,
        new Questions() {id = 5,
        question = "Use the X-band radio telecommunications system to measure Psyche's gravity field to high precision.",
        answer = "Radio Science"}
    };

    public static Questions[] Level2Questions = {
        new Questions() {id = 1,
        question = "When does the Psyche spaceraft stop orbiting the Psyche asteroid?",
        answer = ""} ,
        new Questions() {id = 2,
        question = "Provides high-resolution images using filters to discriminate between Psyche's metallic and silicate constituents.",
        answer = ""} ,
        new Questions() {id = 3,
        question = "What university is the mission led by?",
        answer = ""} ,
        new Questions() {id = 4,
        question = "When was the Psyche asteroid discovered?",
        answer = ""} ,
        new Questions() {id = 5,
        question = "What astronomer discovered the Psyche asteroid?",
        answer = ""}
    };

    public static Questions[] Level3Questions = {
        new Questions() {id = 1,
        question = "How far is the Psyche asteroid from the sun?",
        answer = ""} ,
        new Questions() {id = 2,
        question = "How long is a year on Psyche?",
        answer = ""} ,
        new Questions() {id = 3,
        question = "How long is a day on Psyche?",
        answer = ""} ,
        new Questions() {id = 4,
        question = "Where is the Psyche asteroid?",
        answer = ""} ,
        new Questions() {id = 5,
        question = "Use the X-band radio telecommunications system to measure Psyche's gravity field to high precision.",
        answer = ""}
    };

    public static Questions[] Level4Questions = {
        new Questions() {id = 1,
        question = "What is the length of the Psyche spacecraft?",
        answer = ""} ,
        new Questions() {id = 2,
        question = "What is the width of the Psyche spacecraft?",
        answer = ""} ,
        new Questions() {id = 3,
        question = "Will detect, measure and map Psyche's elemental composition.",
        answer = ""} ,
        new Questions() {id = 4,
        question = "What kind of propellant will the Psyche spacecraft use?",
        answer = ""} ,
        new Questions() {id = 5,
        question = "How far will the Psyche spacecraft travel?",
        answer = ""}
    };



}
