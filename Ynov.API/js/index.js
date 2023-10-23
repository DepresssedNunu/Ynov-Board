const modal = document.getElementById("modal");
const button = document.getElementsByClassName("button")
const mainConsole = document.getElementById("main-console")


mainConsole.addEventListener("click", function (event) {
    const target = event.target;
    if (target.classList.contains("button")) {
        event.preventDefault();
        toggleFormVisibility(target.nextElementSibling);
    }
});



function toggleFormVisibility(form) {
    if (form.style.display === "none" || form.style.display === "") {
        form.style.display = "block";
    } else {
        form.style.display = "none";
    }
}



async function updateBoard(apiEndpoint) {
    getBoardsAndCards();
}

function displayBoardsAndCards(response) {
    modal.innerHTML = ""; // Clear previous content
    response.forEach(board => {
        const boardHTML = `
      <div class="board">
        <h3>${board.name} : ${board.id}</h3>
        ${board.cards.map(card => `
          <div class="card">
            <h4>${card.name}</h4>
            <p>${card.description}</p>
          </div>
        `).join('')}
      </div>
    `;
        modal.innerHTML += boardHTML;
    });
}

async function getBoardsAndCards() {
    const apiEndpoint = "/board/all";
    await fetch(apiEndpoint)
        .then(response => response.json())
        .then(response => displayBoardsAndCards(response));
}

updateBoard();
