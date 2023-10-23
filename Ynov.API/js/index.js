const nameForm = document.getElementById("nameForm");
const nameInput = document.getElementById("nameInput");
const modal = document.getElementById("modal");
const addButton = document.querySelector(".add-button");
const deleteButton = document.querySelector(".delete-button");

nameForm.addEventListener("submit", async function (event) {
    event.preventDefault();
    const name = nameInput.value;
    if (name) {
        const apiEndpoint = `/add/${name}`;
        await fetch(apiEndpoint, {
            method: 'POST',
        })
    }
    getBoardsAndCards();
});

deleteButton.addEventListener("click", async function (event) {
    console.log("ahahah")
    event.preventDefault();
    const apiEndpoint = `/board/1/delete`;
    await fetch(apiEndpoint, {
        method: 'DELETE',
    })
    
    getBoardsAndCards();
});

addButton.addEventListener("click", function (event) {
    event.preventDefault();
    addBoardForm();
});

async function updateBoard(apiEndpoint) {
    getBoardsAndCards();
}
function addBoardForm() {
    if (nameForm.style.display === "none") {
        nameForm.style.display = "block";
    } else {
        nameForm.style.display = "none";
    }
}


function displayBoardsAndCards(response) {
    const modal = document.getElementById("modal");
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
        .then(response => displayBoardsAndCards(response))
}

updateBoard();