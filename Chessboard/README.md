# Blazor Chessboard component
This is a simple chessboard Blazor component. It renders a chessboard including pieces in your application. Pieces can be moved according to the chess rules till the game is in mate or stale mate. 

# Getting started
1. Install the CWK.BlazorComponents.Chessboard package from NuGet. 
2. Build the application
3. Go to the **.\bin\Debug\netstandard2.0\dist\_content\CWK.BlazorComponents.Chessboard folder**
4. Copy the **img** folder to your **wwwroot** folder. If you already have an *img* folder there, then copy only the contents of the **img** folder from the mentioned location.
5. In the **_Imports.razor** file add the following line of code: `@using CWK.BlazorComponents.Chessboard;`
6. If you're using a VS Blazor template go to the **Index.razor** file and add the following markup:
```
@page "/";

<h4>Position: @Fen</h4>

<CwkChessboard Width="700px"
               Orientation="black"
               FenHasChanged="@SetFen"></CwkChessboard>

@functions {

    protected string Fen { get; set; }
    protected void SetFen(string fen)
    {
        Fen = fen;
    }

}
```
7. Run the application. You should have a working chessboard.

# Functionalities
The CwkChessboard component provides basic chessboard functionality.
1. You need to provide the desired **Width** of your chessboard. See previous code snippet for reference.
2. You can provide a starting **Fen** string if you want to render the board at a specific position. 
```
<CwkChessboard Width="700px"
               Orientation="black"
               FenHasChanged="@SetFen"
               Fen="r1bqkbnr/pppp1ppp/2n5/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R b KQkq - 0 3"></CwkChessboard>
```
This code snippet will start the board with the Ruy Lopez opening. 
The default starting position will be used if you don't provide FEN string

3. You can provoide information about the board **Orientation**. See above code snippet for reference. Default orientation will be *white* if no orientation is provided.

4. The **CWKChessboard** component pushes the new FEN after each move through an `EventCallback<string>` by exposing it via the **FenHasChanged** property. In this example we have a basic method in the **Index.razor** file that is attached to the event callback. It always takes the new value and updates the correspondent property, which is used in the heading. 
```
<h4>Position: @Fen</h4>

<CwkChessboard Width="700px"
               Orientation="black"
               FenHasChanged="@SetFen"
               Fen="r1bqkbnr/pppp1ppp/2n5/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R b KQkq - 0 3"></CwkChessboard>

@functions {

    protected string Fen { get; set; }
    protected void SetFen(string fen)
    {
        Fen = fen;
    }

}
```
# Use cases
As far as I can tell this component might be very useful if you want to build something like a tactics trainer app with Blazor, or an opening book or simple analysis board. 

# Limitations
- This component has been tested only with client-side Blazor. The component should also work when hosted server side but in that case  one would need to add all the needed JS files to the **wwwroot** folder and reference them in the markup. 
- For now a pawn can only be promoted to a Queen. 

# Feedback
I am a really bad product owner. So any feedback on what features you would expect from a chessboard component would be highly appreciated. You can contact me via Twitter: **@danpdc** or my personal blog. 
