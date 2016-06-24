using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace DialoguerEditor
{
    [System.Serializable]
    public class DialogueEditorVariablesContainer
    {
        public List<DialogueEditorVariableObject> variables;
        public int selection;

        public DialogueEditorVariablesContainer()
        {
            selection = 0;
            variables = new List<DialogueEditorVariableObject>();
        }

        public void addVariable()
        {
            int count = variables.Count;
            variables.Add(new DialogueEditorVariableObject());
            variables[count].id = count;
            selection = variables.Count - 1;
        }

        /// <summary>
        /// 修改删除最后一项为删除选中项
        /// </summary>
        public void removeVariable()
        {
            if (variables.Count < 1) return;
            fixSelection();
            variables.RemoveAt(selection);
            resetVariablesID();
            fixSelection();
        }

        /// <summary>
        /// 修正selection范围 
        /// </summary>
        public void fixSelection()
        {
            if (selection < 0)
            {
                selection = 0;
            }
            else if (selection > (variables.Count - 1))
            {
                selection = variables.Count - 1;
            }
        }
        /// <summary>
        /// 重新列出id
        /// </summary>
        void resetVariablesID()
        {
            for (int i = 0; i < variables.Count; i++)
            {
                variables[i].id = i;
            }
        }
    }
}