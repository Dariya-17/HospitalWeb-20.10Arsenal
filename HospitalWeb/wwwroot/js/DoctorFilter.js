JS
async function loadDoctors() {

    // 1. Вземаме стойностите от полетата
    let search = document.getElementById("search").value;
    let specialty = document.getElementById("specialty").value;
    let sortOrder = document.getElementById("sortOrder").value;
    // 2. Подготвяме адреса на заявката
    let params = new URLSearchParams({
        search: search,
        specialty: specialty,
        sortOrder: sortOrder
    });
    // 3. Изпращаме заявката към контролера
    let response = await fetch(
        "/Doctor/GetFilteredDoctor?" + params
    );

    // 4. Преобразуваме JSON отговора в JavaScript обекти
    let doctors = await response.json();
    // 5. Изчистваме старите редове от таблицата
   let tbody = document.getElementById("doctorsTable");
    tbody.innerHTML = "";
    // 6. Добавяме ред за всеки намерен лекар
    doctors.forEach(function (doctor) {
        let row = document.createElement("tr");
        [doctor.firstName, doctor.lastName,
        doctor.specialty, doctor.hospitalName].forEach(function (value) {
            let cell = document.createElement("td");
            cell.textContent = value ?? "";
            row.appendChild(cell);
        });
        // Колона с линкове за действия
        let actions = document.createElement("td");
        ["Edit", "Details", "Delete"].forEach(function (action, index) {

            if (index > 0) {

                actions.appendChild(document.createTextNode(" | "));

            }
            let link = document.createElement("a");
            link.href = "/Doctor/" + action + "/" + doctor.id;
            link.textContent = action;
            actions.appendChild(link);
        });
        row.appendChild(actions);
        tbody.appendChild(row);

    });

}

// Автоматично филтриране

document.getElementById("search")

    .addEventListener("input", loadDoctors);

document.getElementById("specialty")

    .addEventListener("input", loadDoctors);

document.getElementById("sortOrder")

    .addEventListener("change", loadDoctors);

// Изчистване на филтрите

document.getElementById("clearFilters")

    .addEventListener("click", function () {

        document.getElementById("search").value = "";

        document.getElementById("specialty").value = "";

        document.getElementById("sortOrder").value = "name_asc";

        loadDoctors();

    });
