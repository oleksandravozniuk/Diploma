#pragma checksum "C:\Users\Oleksandra_Vozniuk\Desktop\or-tools_VisualStudio2019-64bit_v8.0.8283\examples\dotnet\DiplomaApp\Diploma\PresentationLevel\Views\MyProblems\MyProblemsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B058F4440FF165B7637DC420FA801D3B"
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
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
            public static void Set_Windows_UI_Xaml_FrameworkElement_Tag(global::Windows.UI.Xaml.FrameworkElement obj, global::System.Object value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = (global::System.Object) global::Windows.UI.Xaml.Markup.XamlBindingHelper.ConvertValue(typeof(global::System.Object), targetNullValue);
                }
                obj.Tag = value;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class MyProblemsPage_obj7_Bindings :
            global::Windows.UI.Xaml.IDataTemplateExtension,
            global::Windows.UI.Xaml.Markup.IDataTemplateComponent,
            global::Windows.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IMyProblemsPage_Bindings
        {
            private global::ProblemManager.Dtos.IndividualProblemDto dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);
            private bool removedDataContextHandler = false;

            // Fields for each control that has bindings.
            private global::System.WeakReference obj7;
            private global::Windows.UI.Xaml.Controls.TextBlock obj8;
            private global::Windows.UI.Xaml.Controls.Button obj9;
            private global::Windows.UI.Xaml.Controls.Button obj10;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj8TextDisabled = false;
            private static bool isobj9TagDisabled = false;
            private static bool isobj10TagDisabled = false;

            public MyProblemsPage_obj7_Bindings()
            {
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 31 && columnNumber == 66)
                {
                    isobj8TextDisabled = true;
                }
                else if (lineNumber == 32 && columnNumber == 69)
                {
                    isobj9TagDisabled = true;
                }
                else if (lineNumber == 33 && columnNumber == 71)
                {
                    isobj10TagDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 7: // Views\MyProblems\MyProblemsPage.xaml line 30
                        this.obj7 = new global::System.WeakReference((global::Windows.UI.Xaml.Controls.StackPanel)target);
                        break;
                    case 8: // Views\MyProblems\MyProblemsPage.xaml line 31
                        this.obj8 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 9: // Views\MyProblems\MyProblemsPage.xaml line 32
                        this.obj9 = (global::Windows.UI.Xaml.Controls.Button)target;
                        break;
                    case 10: // Views\MyProblems\MyProblemsPage.xaml line 33
                        this.obj10 = (global::Windows.UI.Xaml.Controls.Button)target;
                        break;
                    default:
                        break;
                }
            }

            public void DataContextChangedHandler(global::Windows.UI.Xaml.FrameworkElement sender, global::Windows.UI.Xaml.DataContextChangedEventArgs args)
            {
                 if (this.SetDataRoot(args.NewValue))
                 {
                    this.Update();
                 }
            }

            // IDataTemplateExtension

            public bool ProcessBinding(uint phase)
            {
                throw new global::System.NotImplementedException();
            }

            public int ProcessBindings(global::Windows.UI.Xaml.Controls.ContainerContentChangingEventArgs args)
            {
                int nextPhase = -1;
                ProcessBindings(args.Item, args.ItemIndex, (int)args.Phase, out nextPhase);
                return nextPhase;
            }

            public void ResetTemplate()
            {
                Recycle();
            }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
                switch(phase)
                {
                    case 0:
                        nextPhase = -1;
                        this.SetDataRoot(item);
                        if (!removedDataContextHandler)
                        {
                            removedDataContextHandler = true;
                            (this.obj7.Target as global::Windows.UI.Xaml.Controls.StackPanel).DataContextChanged -= this.DataContextChangedHandler;
                        }
                        this.initialized = true;
                        break;
                }
                this.Update_((global::ProblemManager.Dtos.IndividualProblemDto) item, 1 << phase);
            }

            public void Recycle()
            {
            }

            // IMyProblemsPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::ProblemManager.Dtos.IndividualProblemDto)newDataRoot;
                    return true;
                }
                return false;
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::ProblemManager.Dtos.IndividualProblemDto obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_Name(obj.Name, phase);
                        this.Update_Id(obj.Id, phase);
                    }
                }
            }
            private void Update_Name(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\MyProblems\MyProblemsPage.xaml line 31
                    if (!isobj8TextDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj8, obj, null);
                    }
                }
            }
            private void Update_Id(global::System.Int32 obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\MyProblems\MyProblemsPage.xaml line 32
                    if (!isobj9TagDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_FrameworkElement_Tag(this.obj9, obj, null);
                    }
                    // Views\MyProblems\MyProblemsPage.xaml line 33
                    if (!isobj10TagDisabled)
                    {
                        XamlBindingSetters.Set_Windows_UI_Xaml_FrameworkElement_Tag(this.obj10, obj, null);
                    }
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.18362.1")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\MyProblems\MyProblemsPage.xaml line 13
                {
                    this.commandBar = (global::Windows.UI.Xaml.Controls.CommandBar)(target);
                }
                break;
            case 3: // Views\MyProblems\MyProblemsPage.xaml line 15
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.NewProblem_Click;
                }
                break;
            case 4: // Views\MyProblems\MyProblemsPage.xaml line 20
                {
                    this.MyProblemsTabView = (global::Microsoft.UI.Xaml.Controls.TabView)(target);
                    ((global::Microsoft.UI.Xaml.Controls.TabView)this.MyProblemsTabView).TabCloseRequested += this.Tabs_TabCloseRequested;
                }
                break;
            case 5: // Views\MyProblems\MyProblemsPage.xaml line 27
                {
                    this.MyProblemsList = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 9: // Views\MyProblems\MyProblemsPage.xaml line 32
                {
                    global::Windows.UI.Xaml.Controls.Button element9 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element9).Click += this.MoreButton_Click;
                }
                break;
            case 10: // Views\MyProblems\MyProblemsPage.xaml line 33
                {
                    global::Windows.UI.Xaml.Controls.Button element10 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element10).Click += this.DeleteButton_Click;
                }
                break;
            case 11: // Views\MyProblems\MyProblemsPage.xaml line 42
                {
                    this.ShellTitlebarInset = (global::Windows.UI.Xaml.Controls.Grid)(target);
                }
                break;
            case 12: // Views\MyProblems\MyProblemsPage.xaml line 45
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
            switch(connectionId)
            {
            case 7: // Views\MyProblems\MyProblemsPage.xaml line 30
                {                    
                    global::Windows.UI.Xaml.Controls.StackPanel element7 = (global::Windows.UI.Xaml.Controls.StackPanel)target;
                    MyProblemsPage_obj7_Bindings bindings = new MyProblemsPage_obj7_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(element7.DataContext);
                    element7.DataContextChanged += bindings.DataContextChangedHandler;
                    global::Windows.UI.Xaml.DataTemplate.SetExtensionInstance(element7, bindings);
                    global::Windows.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element7, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

