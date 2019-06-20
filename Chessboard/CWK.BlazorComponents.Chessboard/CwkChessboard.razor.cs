using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CWK.BlazorComponents.Chessboard
{
    public class CwkChessboardModel : ComponentBase
    {
        [Inject] protected IJSRuntime JsRuntime { get; set; }
        [Parameter] protected string Width { get; set; }

        private static string _fen;
        [Parameter] protected string Fen
        {
            get { return _fen; }
            set { _fen = value; }
        }

        private static string _orientation;
        [Parameter] protected string Orientation
        {
            get { return _orientation; }
            set { _orientation = value; }
        }

        private static EventCallback<string> _fenChanged;
        [Parameter] protected EventCallback<string> FenHasChanged { get { return _fenChanged; } set { _fenChanged = value; } }

        private static Stack<string> _previousFens = new Stack<string>();
        private static Stack<string> _forwardFens = new Stack<string>();

        public CwkChessboardModel()
        {
            Fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
            Orientation = "white";
            
        }

        protected override async Task OnAfterRenderAsync()
        {
            await JsRuntime.InvokeAsync<object>("renderBoard", Fen, Orientation);

        }

        [JSInvokable]
        public static void SetCurrentFen(string fen)
        {
            if (_forwardFens.Count > 0)
                _forwardFens.Clear();
            _previousFens.Push(_fen);
            _fen = fen;
            _fenChanged.InvokeAsync(_fen);

        }

        protected void Back()
        {
            if (_previousFens.Count > 0)
            {
                _forwardFens.Push(Fen);
                Fen = _previousFens.Pop();
                FenHasChanged.InvokeAsync(Fen);
            }
           
        }

        protected void Forward()
        {
            if (_forwardFens.Count > 0)
            {
                _previousFens.Push(Fen);
                Fen = _forwardFens.Pop();
                FenHasChanged.InvokeAsync(Fen);
            }
            
        }
    }
}
