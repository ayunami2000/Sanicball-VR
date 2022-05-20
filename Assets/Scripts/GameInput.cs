using Sanicball.Data;
using SanicballCore;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

namespace Sanicball
{
    public static class GameInput
    {
        public static bool KeyboardDisabled = true;

        public static string GetInputName(Keybind keybind)
        {
            switch (keybind)
            {
                case Keybind.Forward:
                case Keybind.Left:
                case Keybind.Back:
                case Keybind.Right:
                    return SteamVR_Actions.sanicball_Move.localizedOriginName;
                case Keybind.CameraUp:
                case Keybind.CameraLeft:
                case Keybind.CameraDown:
                case Keybind.CameraRight:
                    return SteamVR_Actions.sanicball_Look.localizedOriginName;
                case Keybind.Brake:
                    return SteamVR_Actions.sanicball_Brake.localizedOriginName;
                case Keybind.Jump:
                    return SteamVR_Actions.sanicball_Jump.localizedOriginName;
                case Keybind.Respawn:
                    return SteamVR_Actions.sanicball_Respawn.localizedOriginName;
                case Keybind.Menu:
                    return SteamVR_Actions.sanicball_Select.localizedOriginName;
                case Keybind.NextSong:
                    return SteamVR_Actions.sanicball_Next_Track.localizedOriginName;
                case Keybind.Chat:
                    return "(not implemented...)";
            }
            return "N/A";
        }

        public static string GetControlTypeName(ControlType ctrlType)
        {
            switch (ctrlType)
            {
                case ControlType.Keyboard:
                    return "Keyboard";

                case ControlType.Joystick1:
                    return "Joystick #1";

                case ControlType.Joystick2:
                    return "Joystick #2";

                case ControlType.Joystick3:
                    return "Joystick #3";

                case ControlType.Joystick4:
                    return "Joystick #4";
            }
            return null;
        }

        public static Vector3 MovementVector(ControlType ctrlType)
        {
            if (ctrlType != ControlType.Joystick1) return Vector3.zero;
            Vector2 vec2 = SteamVR_Actions.sanicball_Move.axis;
            //return new Vector3(vec2.x * 1.2f, 0, vec2.y * 1.2f);
            return new Vector3(vec2.x, 0, vec2.y);
        }

        public static Vector2 CameraVector(ControlType ctrlType)
        {
            if (ctrlType != ControlType.Joystick1) return Vector2.zero;
            Vector2 vec2 = SteamVR_Actions.sanicball_Look.axis;
            //return new Vector2(vec2.x * 1.2f, vec2.y * 1.2f);
            return vec2;
        }

        public static bool UIUp(ControlType ctrlType)
        {
            if (ctrlType != ControlType.Joystick1) return false;
            return SteamVR_Actions.sanicball_Look.axis.y >= 0.8f;
        }

        public static bool UIDown(ControlType ctrlType)
        {
            if (ctrlType != ControlType.Joystick1) return false;
            return SteamVR_Actions.sanicball_Look.axis.y <= -0.8f;
        }

        public static bool UILeft(ControlType ctrlType)
        {
            if (ctrlType != ControlType.Joystick1) return false;
            return SteamVR_Actions.sanicball_Look.axis.x <= -0.8f;
        }

        public static bool UIRight(ControlType ctrlType)
        {
            if (ctrlType != ControlType.Joystick1) return false;
            return SteamVR_Actions.sanicball_Look.axis.x >= 0.8f;
        }

        public static bool IsBraking(ControlType ctrlType)
        {
            if (ctrlType != ControlType.Joystick1) return false;
            return SteamVR_Actions.sanicball_Brake.state;
        }

        public static bool IsJumping(ControlType ctrlType)
        {
            if (ctrlType != ControlType.Joystick1) return false;
            return SteamVR_Actions.sanicball_Jump.state;
        }

        public static bool IsRespawning(ControlType ctrlType)
        {
            if (ctrlType != ControlType.Joystick1) return false;
            return SteamVR_Actions.sanicball_Respawn.state && !SteamVR_Actions.sanicball_Respawn.lastState;
        }

        public static bool IsOpeningMenu(ControlType ctrlType)
        {
            if (ctrlType != ControlType.Joystick1) return false;
            return SteamVR_Actions.sanicball_Select.state && !SteamVR_Actions.sanicball_Select.lastState;
        }

        public static bool IsClicking(ControlType ctrlType)
        {
            if (ctrlType != ControlType.Joystick1) return false;
            return SteamVR_Actions.sanicball_Click.state && !SteamVR_Actions.sanicball_Click.lastState;
        }

        public static bool IsChangingSong()
        {
            return SteamVR_Actions.sanicball_Next_Track.state && !SteamVR_Actions.sanicball_Next_Track.lastState;
        }

        public static bool IsOpeningChat()
        {
            return false;
        }

        public static bool IsPausing()
        {
            return SteamVR_Actions.sanicball_Menu.state && !SteamVR_Actions.sanicball_Menu.lastState;
        }
    }
}