﻿#pragma checksum "C:\Users\Oleksandra\Desktop\Diploma\Diploma\PresentationLevel\Pages\MyProblems\MyProblemsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B4AB29D74AAF6C1F684BD0BCED36F495"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PresentationLevel.Pages
{
    partial class MyProblemsPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Pages\MyProblems\MyProblemsPage.xaml line 12
                {
                    this.commandBar = (global::Windows.UI.Xaml.Controls.CommandBar)(target);
                }
                break;
            case 3: // Pages\MyProblems\MyProblemsPage.xaml line 14
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.NewProblem_Click;
                }
                break;
            case 4: // Pages\MyProblems\MyProblemsPage.xaml line 19
                {
                    this.MyProblemsTabView = (global::Microsoft.UI.Xaml.Controls.TabView)(target);
                    ((global::Microsoft.UI.Xaml.Controls.TabView)this.MyProblemsTabView).TabCloseRequested += this.Tabs_TabCloseRequested;
                }
                break;
            case 5: // Pages\MyProblems\MyProblemsPage.xaml line 26
                {
                    this.ShellTitlebarInset = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 6: // Pages\MyProblems\MyProblemsPage.xaml line 29
                {
                    this.CustomDragRegion = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

