document.addEventListener("DOMContentLoaded", function () {
 
    var patient = {
        name: patient.name,
        email: patient.email,
        phone: patient.phone,
        address: patient.address
    };

    function displayPatientInfo() {
        var patientInfoDiv = document.getElementById("patientInfo");
        patientInfoDiv.innerHTML = `
            <div class="item">
                <i class="fas fa-user"></i>
                <span>Name: ${patient.name}</span>
            </div>
            <div class="item">
                <i class="fas fa-envelope"></i>
                <span>Email: ${patient.email}</span>
            </div>
            <div class="item">
                <i class="fas fa-phone"></i>
                <span>Phone: ${patient.phone}</span>
            </div>
            <div class="item">
                <i class="fas fa-map-marker-alt"></i>
                <span>Address: ${patient.address}</span>
            </div>
        `;
    }

    displayPatientInfo();
});
