using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DPIUNAWARE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string valueName = "";
        private void button2_Click(object sender, EventArgs e)
        {
            string registryPath = @"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers";
            
            string valueData = "DPIUNAWARE";

            // 打开注册表项
            RegistryKey key = Registry.CurrentUser.OpenSubKey(registryPath, true);

            // 设置字符串值
            key.SetValue(valueName, valueData);

            // 关闭注册表项
            key.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog1.FileName;

                // 获取文件的扩展名
                string fileExtension = System.IO.Path.GetExtension(selectedFilePath);

                if (fileExtension.Equals(".exe", StringComparison.OrdinalIgnoreCase))
                {
                    // 如果选择的文件是可执行文件，则返回具体路径
                    string exeFilePath = System.IO.Path.GetFullPath(selectedFilePath);
                    Console.WriteLine("选择的文件是可执行文件，路径为：" + exeFilePath);
                    label2.Text = exeFilePath;
                    valueName = exeFilePath;
                }
                else if (fileExtension.Equals(".lnk", StringComparison.OrdinalIgnoreCase))
                {
                    // 如果选择的文件是快捷方式，则提取具体路径
                    string targetFilePath = GetShortcutTarget(selectedFilePath);
                    Console.WriteLine("选择的文件是快捷方式，目标路径为：" + targetFilePath);
                }
                else
                {
                    Console.WriteLine("选择的文件类型不受支持。");
                }

            }
        }
        // 获取快捷方式的目标路径
    static string GetShortcutTarget(string shortcutPath)
        {
            dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
            dynamic shortcut = shell.CreateShortcut(shortcutPath);
            return shortcut.TargetPath;
        }
    }
}
