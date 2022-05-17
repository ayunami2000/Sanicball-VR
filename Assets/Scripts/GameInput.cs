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
            return "VR Controller";
        }

        public static Vector3 MovementVector(ControlType ctrlType)
        {
            Vector2 vec2 = SteamVR_Actions.sanicball_Move.axis;
            return new Vector3(vec2.x, 0, vec2.y);
        }

        public static Vector2 CameraVector(ControlType ctrlType)
        {
            return SteamVR_Actions.sanicball_Look.axis;
        }

        public static bool UIUp(ControlType ctrlType)
        {
            return SteamVR_Actions.sanicball_Look.axis.y == 1f;
        }

        public static bool UIDown(ControlType ctrlType)
        {
            return SteamVR_Actions.sanicball_Look.axis.y == -1f;
        }

        public static bool UILeft(ControlType ctrlType)
        {
            return SteamVR_Actions.sanicball_Look.axis.x == -1f;
        }

        public static bool UIRight(ControlType ctrlType)
        {
            return SteamVR_Actions.sanicball_Look.axis.x == 1f;
        }

        public static bool IsBraking(ControlType ctrlType)
        {
            return SteamVR_Actions.sanicball_Brake.state;
        }

        public static bool IsJumping(ControlType ctrlType)
        {
            return SteamVR_Actions.sanicball_Jump.state;
        }

        public static bool IsRespawning(ControlType ctrlType)
        {
            return SteamVR_Actions.sanicball_Respawn.state && !SteamVR_Actions.sanicball_Respawn.lastState;
        }

        public static bool IsOpeningMenu(ControlType ctrlType)
        {
            return SteamVR_Actions.sanicball_Select.state && !SteamVR_Actions.sanicball_Select.lastState;
        }

        public static bool IsChangingSong()
        {
            return SteamVR_Actions.sanicball_Next_Track.state && !SteamVR_Actions.sanicball_Next_Track.lastState;
        }

        public static bool IsOpeningChat()
        {
            return false;
        }
    }
}