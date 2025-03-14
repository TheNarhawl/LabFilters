﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabFilters
{
    public class HotkeysManager
    {
        private Stack<Bitmap> undoStack;
        private Stack<Bitmap> redoStack;
        private Action undoAction;
        private Action redoAction;
        private Action pasteFromClipboard;

        public HotkeysManager(Stack<Bitmap> undoStack, Stack<Bitmap> redoStack, Action undoAction, Action redoAction, Action pasteFromClipboard)
        {
            this.undoStack = undoStack;
            this.redoStack = redoStack;
            this.undoAction = undoAction;
            this.redoAction = redoAction;
            this.pasteFromClipboard = pasteFromClipboard;
        }

        public void HandleHotkey(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z)
            {
                undoAction();
                e.SuppressKeyPress = true;
            }

            else if (e.Control && e.KeyCode == Keys.Y)
            {
                redoAction();
                e.SuppressKeyPress = true; 
            }

            else if (e.Control && e.KeyCode == Keys.V)
            {
                pasteFromClipboard();
                e.SuppressKeyPress = true;
            }
        }
    }
}
