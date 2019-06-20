


function renderBoard(position, orientation) {

    var board,
        game = new Chess(position);

    function onDragStart(source, piece, position, orientation) {
        // do not pick up pieces if the game is over
        if (game.game_over()) return false;

        // only pick up pieces for the side to move
        if ((game.turn() === 'w' && piece.search(/^b/) !== -1) ||
            (game.turn() === 'b' && piece.search(/^w/) !== -1)) {
            return false;
        }
    }

    function onDrop(source, target) {
        // see if the move is legal
        var move = game.move({
            from: source,
            to: target,
            promotion: 'q' // NOTE: always promote to a queen for example simplicity
        });

        // illegal move
        if (move === null) return 'snapback';

        updateStatus();
    }

    // update the board position after the piece snap
    // for castling, en passant, pawn promotion
    function onSnapEnd() {
        board.position(game.fen());
        DotNet.invokeMethodAsync('CWK.BlazorComponents.Chessboard', 'SetCurrentFen', game.fen());
    }

    function updateStatus() {

        document.getElementById("fen").value = game.fen();
    }

    var config = {
        orientation: orientation,
        draggable: true,
        position: position,
        onDragStart: onDragStart,
        onDrop: onDrop,
        onSnapEnd: onSnapEnd
    };
    board = ChessBoard('board', config);

    updateStatus();
}

