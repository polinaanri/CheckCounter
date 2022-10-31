
using System;
using System.Configuration;
using System.Data;

namespace CheckCounterMain
{
    public static class UserSettings
    {
        public static AppUserSettings userSettings = new AppUserSettings();
    }
    public class AppUserSettings : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
        public List<ErrorDescription> reportErrorPatterns
        {
            get
            {
                return (this["reportErrorPatterns"] as List<ErrorDescription>);
            }
            set
            {
                this["reportErrorPatterns"] = value;
            }
        }




        [UserScopedSetting()]
        [DefaultSettingValueAttribute("false")]
        public bool readVolumes
        {
            get
            {
                return (bool)this["readVolumes"];
            }
            set
            {
                this["readVolumes"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValueAttribute("true")]
        public bool showMajor
        {
            get
            {
                return (bool)this["showMajor"];
            }
            set
            {
                this["showMajor"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValueAttribute("")]
        public string countersFilePath
        {
            get
            {
                return (string)this["countersFilePath"];
            }
            set
            {
                this["countersFilePath"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValueAttribute("")]
        public string countersFolderPath
        {
            get
            {
                return (string)this["countersFolderPath"];
            }
            set
            {
                this["countersFolderPath"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValueAttribute("")]
        public string volumesFilePath
        {
            get
            {
                return (string)this["volumesFilePath"];
            }
            set
            {
                this["volumesFilePath"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValueAttribute("20")]
        public int Tz
        {
            get
            {
                return (int)this["Tz"];
            }
            set
            {
                this["Tz"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValueAttribute("3")]
        public int Ty
        {
            get
            {
                return (int)this["Ty"];
            }
            set
            {
                this["Ty"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValueAttribute("4")]
        public int Gx
        {
            get
            {
                return (int)this["Gx"];
            }
            set
            {
                this["Gx"] = value;
            }
        }

        [UserScopedSetting()]
        [SettingsSerializeAs(System.Configuration.SettingsSerializeAs.Xml)]
        public List<BuildingReport> buildingReport
        {
            get
            {
                return (this["buildingReport"] as List<BuildingReport>);
            }
            set
            {
                this["buildingReport"] = value;
            }
        }
    }
}
