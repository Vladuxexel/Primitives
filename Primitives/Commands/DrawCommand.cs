using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Primitives
{
    class DrawCommand : TypedCommand<MainWindowVM>
    {
        protected override void Execute(MainWindowVM mainWindowVM)
        {
            //если нет объекта то создать в зависимости от типа
            //игнорим близкие точки на уровне базового объекта
            //пихаем в объект точки пока он не врент флаг, того что он законил построение.


            mainWindowVM.clicks++;
            if (mainWindowVM.isRectangle)
            {
                if (mainWindowVM.clicks == 1 && mainWindowVM.viewport.CursorOnConstructionPlanePosition.HasValue)
                {
                    mainWindowVM.tempCoordinates.Add(mainWindowVM.viewport.CursorOnConstructionPlanePosition.Value);
                }
                else if (mainWindowVM.clicks == 2 && mainWindowVM.viewport.CursorOnConstructionPlanePosition.HasValue)
                {
                    mainWindowVM.tempCoordinates.Add(mainWindowVM.viewport.CursorOnConstructionPlanePosition.Value);
                    var rect = new WireRectangle(mainWindowVM.tempCoordinates[0], mainWindowVM.tempCoordinates[1]);
                    mainWindowVM.viewport.Children.Add(rect);
                    mainWindowVM.tempCoordinates.Clear();
                    mainWindowVM.clicks = 0;
                    mainWindowVM.Model = mainWindowVM.modelGroup;
                }
            }
            else if (mainWindowVM.isPolygon)
            {
                if (mainWindowVM.tempCoordinates.Count < 2 && mainWindowVM.viewport.CursorOnConstructionPlanePosition.HasValue)
                {
                    mainWindowVM.tempCoordinates.Add(mainWindowVM.viewport.CursorOnConstructionPlanePosition.Value);

                    //if (mainWindowVM.tempCoordinates.Count == 2)
                    //{
                    //    WirePolygon.DrawLines(mainWindowVM.tempCoordinates);
                    //}
                }
                else if (mainWindowVM.tempCoordinates.Count <= 2 && mainWindowVM.viewport.CursorOnConstructionPlanePosition.HasValue)
                {
                    if (Calculator.IsInRadius(mainWindowVM.tempCoordinates[mainWindowVM.tempCoordinates.Count-1],mainWindowVM.tempCoordinates[0], 1))
                    {
                        mainWindowVM.tempCoordinates.RemoveAt(mainWindowVM.tempCoordinates.Count - 1);
                        var poly = new WirePolygon(mainWindowVM.tempCoordinates);
                        mainWindowVM.viewport.Children.Add(poly);
                        mainWindowVM.tempCoordinates.Clear();
                        mainWindowVM.clicks = 0;
                        mainWindowVM.Model = mainWindowVM.modelGroup;
                    }
                    else
                    {
                        mainWindowVM.tempCoordinates.Add(mainWindowVM.viewport.CursorOnConstructionPlanePosition.Value);
                    }
                }
            }
        }
    }
}
