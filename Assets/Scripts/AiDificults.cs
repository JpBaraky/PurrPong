using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDificults: MonoBehaviour {
    public enum AI {
        Stage1, Stage2, Stage3, Stage4, Stage5,
    }
    public static float GetReactionTime(AI ai) {
        switch(ai) {
            default:
            return 1;
            case AI.Stage1:
            return 0.5f;
            case AI.Stage2:
            return 0.4f;
            case AI.Stage3:
            return 0.3f;
            case AI.Stage4:
            return 0.2f;            
            case AI.Stage5:
            return 0.1f;
        }

    }
    public static float GetErrorMargin(AI ai) {
        switch(ai) {
            default:
            return 1;
            case AI.Stage1:
            return 0.5f;
            case AI.Stage2:
            return 0.4f;
            case AI.Stage3:
            return 0.3f;
            case AI.Stage4:
            return 0.2f;
            case AI.Stage5:
            return 0.1f;
        }
    }
    public static float GetSpeed(AI ai) {
        switch(ai) {
            default:
            return 1;
            case AI.Stage1:
            return 5;
            case AI.Stage2:
            return 6;
            case AI.Stage3:
            return 7;
            case AI.Stage4:
            return 8;
            case AI.Stage5:
            return 9;
        }
    }

}
