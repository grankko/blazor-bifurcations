using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBifurcations.Client.Services
{
    public class JavaScriptService
    {
        private readonly IJSRuntime _jsRuntime;

        public JavaScriptService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public void ClearCanvas(string elementId)
        {
            _jsRuntime.InvokeAsync<bool>("clearCanvas", elementId);
        }

        public void DrawCellOnCanvas(string elementId, double x, double y, double cellSize, string color)
        {
            _jsRuntime.InvokeAsync<bool>("drawOnCanvas", elementId, x, y, cellSize, color);
        }

        public void ResizeCanvas(string elementId)
        {
            _jsRuntime.InvokeAsync<bool>("resizeCanvas", elementId);
        }
    }
}
