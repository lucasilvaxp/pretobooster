using Microsoft.Win32;
using System;

namespace PretoBoost.Services
{
    public static class RegistryService
    {
        public static bool SetValue(string keyPath, string valueName, object value, RegistryValueKind valueKind = RegistryValueKind.DWord)
        {
            try
            {
                LogService.Log($"Registry: Definindo {keyPath}\\{valueName} = {value}");

                RegistryKey? baseKey = GetBaseKey(ref keyPath);
                if (baseKey == null) return false;

                using RegistryKey? key = baseKey.CreateSubKey(keyPath, true);
                if (key == null)
                {
                    LogService.LogError($"Registry: {keyPath}\\{valueName}", "Não foi possível criar/abrir a chave");
                    return false;
                }

                key.SetValue(valueName, value, valueKind);
                LogService.LogSuccess($"Registry: {keyPath}\\{valueName}");
                return true;
            }
            catch (Exception ex)
            {
                LogService.LogError($"Registry: {keyPath}\\{valueName}", ex.Message);
                return false;
            }
        }

        public static object? GetValue(string keyPath, string valueName, object? defaultValue = null)
        {
            try
            {
                RegistryKey? baseKey = GetBaseKey(ref keyPath);
                if (baseKey == null) return defaultValue;

                using RegistryKey? key = baseKey.OpenSubKey(keyPath);
                return key?.GetValue(valueName, defaultValue) ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool DeleteValue(string keyPath, string valueName)
        {
            try
            {
                LogService.Log($"Registry: Deletando {keyPath}\\{valueName}");

                RegistryKey? baseKey = GetBaseKey(ref keyPath);
                if (baseKey == null) return false;

                using RegistryKey? key = baseKey.OpenSubKey(keyPath, true);
                if (key == null) return true; // Já não existe

                key.DeleteValue(valueName, false);
                LogService.LogSuccess($"Registry Delete: {keyPath}\\{valueName}");
                return true;
            }
            catch (Exception ex)
            {
                LogService.LogError($"Registry Delete: {keyPath}\\{valueName}", ex.Message);
                return false;
            }
        }

        public static bool DeleteKey(string keyPath)
        {
            try
            {
                LogService.Log($"Registry: Deletando chave {keyPath}");

                RegistryKey? baseKey = GetBaseKey(ref keyPath);
                if (baseKey == null) return false;

                baseKey.DeleteSubKeyTree(keyPath, false);
                LogService.LogSuccess($"Registry Delete Key: {keyPath}");
                return true;
            }
            catch (Exception ex)
            {
                LogService.LogError($"Registry Delete Key: {keyPath}", ex.Message);
                return false;
            }
        }

        public static bool KeyExists(string keyPath)
        {
            try
            {
                RegistryKey? baseKey = GetBaseKey(ref keyPath);
                if (baseKey == null) return false;

                using RegistryKey? key = baseKey.OpenSubKey(keyPath);
                return key != null;
            }
            catch
            {
                return false;
            }
        }

        private static RegistryKey? GetBaseKey(ref string keyPath)
        {
            if (keyPath.StartsWith("HKEY_LOCAL_MACHINE\\", StringComparison.OrdinalIgnoreCase) ||
                keyPath.StartsWith("HKLM\\", StringComparison.OrdinalIgnoreCase))
            {
                keyPath = keyPath.Substring(keyPath.IndexOf('\\') + 1);
                return Registry.LocalMachine;
            }
            else if (keyPath.StartsWith("HKEY_CURRENT_USER\\", StringComparison.OrdinalIgnoreCase) ||
                     keyPath.StartsWith("HKCU\\", StringComparison.OrdinalIgnoreCase))
            {
                keyPath = keyPath.Substring(keyPath.IndexOf('\\') + 1);
                return Registry.CurrentUser;
            }
            else if (keyPath.StartsWith("HKEY_CLASSES_ROOT\\", StringComparison.OrdinalIgnoreCase) ||
                     keyPath.StartsWith("HKCR\\", StringComparison.OrdinalIgnoreCase))
            {
                keyPath = keyPath.Substring(keyPath.IndexOf('\\') + 1);
                return Registry.ClassesRoot;
            }
            else if (keyPath.StartsWith("HKEY_USERS\\", StringComparison.OrdinalIgnoreCase) ||
                     keyPath.StartsWith("HKU\\", StringComparison.OrdinalIgnoreCase))
            {
                keyPath = keyPath.Substring(keyPath.IndexOf('\\') + 1);
                return Registry.Users;
            }

            // Default to HKLM
            return Registry.LocalMachine;
        }
    }
}
