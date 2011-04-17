using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Modeling;
using AgileFx.AgileModeler.DslPackage.CustomCode.Forms;
using AgileFx.AgileModeler.CustomCode;
using System.Drawing.Imaging;
using System.IO;

namespace AgileFx.AgileModeler.DslPackage.CustomCode.MenuCommands
{
    public class ExportDiagramCommand : DSLMenuCommandImplBase
    {
        Guid commandGuid = new Guid("3c377eba-e82a-45af-9112-a339092d3d4a");
        int commandID = 0x870;

        public override void StatusHandler(CommandSetState state)
        {
            foreach (object selectedObject in state.CurrentSelection)
            {
                if (selectedObject is ClassDiagram)
                {
                    MenuCommand.Visible = MenuCommand.Enabled = true;
                    MenuCommand.Enabled = true;
                    return;
                }
                else
                {
                    MenuCommand.Visible = false;
                    MenuCommand.Enabled = false;
                }
            }
        }

        public override void InvokeHandler(CommandSetState state)
        {
            var dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.DefaultExt = "png";
            dlg.Filter = "Portable Network Graphics (*.png)|*.png|Windows 24-bit bitmap (*.bmp)|*.bmp|JPEG Interchange format (*.jpg)|*.jpg|Graphic Interchange Format (*.gif)|*.gif|Tagged Image File Format (*.tif)|*.tif";
            dlg.AddExtension = true;
            dlg.RestoreDirectory = true;
            dlg.FileName = "AgileModelerDiagram";
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var diagram = state.CurrentDocView.CurrentDiagram;
                var bitmap = diagram.CreateBitmap(diagram.NestedChildShapes, Microsoft.VisualStudio.Modeling.Diagrams.Diagram.CreateBitmapPreference.FavorClarityOverSmallSize);
                ImageFormat format = ImageFormat.Png;

                switch (Path.GetExtension(dlg.FileName).ToLower())
                {
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    case ".jpg":
                        format = ImageFormat.Jpeg;
                        break;
                    case ".gif":
                        format = ImageFormat.Gif;
                        break;
                    case ".tif":
                        format = ImageFormat.Tiff;
                        break;
                    case ".png":
                    default:
                        format = ImageFormat.Png;
                        break;
                }
                bitmap.Save(dlg.FileName, format);
            }
        }

        public override System.ComponentModel.Design.CommandID GetCommandID()
        {
            return new CommandID(commandGuid, commandID);
        }
    }
}
