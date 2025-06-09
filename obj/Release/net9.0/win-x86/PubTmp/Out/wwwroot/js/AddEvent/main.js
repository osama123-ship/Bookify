let menu = document.querySelector('.nav-item3');
let sidebar = document.querySelector('.sidebar');
let content = document.querySelector('.content');
let disable = document.querySelector('.close');


menu.addEventListener('click', ()=> {
    sidebar.classList.add('active');
});


disable.addEventListener('click', ()=> {
    sidebar.classList.remove('active');
});

document.addEventListener('click', (e)=> {
    if (sidebar.classList.contains('active') && !sidebar.contains(e.target) && !menu.contains(e.target)) {
        sidebar.classList.remove('active');
    }
});


const locationInput = document.getElementById('Location');
  const modal = document.getElementById('mapModal');
  let mapInitialized = false;
  let map, marker;

  locationInput.addEventListener('click', () => {
    modal.style.display = 'flex';

    if (!mapInitialized) {
      setTimeout(() => {
        map = L.map('map').setView([26.8206, 30.8025], 6);
        L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
          attribution: '© OpenStreetMap contributors'
        }).addTo(map);

        marker = L.marker([26.8206, 30.8025], { draggable: true }).addTo(map);

        marker.on('dragend', function () {
          const pos = marker.getLatLng();
          reverseGeocode(pos.lat, pos.lng);
        });

        reverseGeocode(26.8206, 30.8025);
        mapInitialized = true;
      }, 300);
    } else {
      setTimeout(() => map.invalidateSize(), 300);
    }
  });

  function closeMapModal() {
    modal.style.display = 'none';
  }

  function reverseGeocode(lat, lng) {
    const url = `https://nominatim.openstreetmap.org/reverse?lat=${lat}&lon=${lng}&format=json`;

    fetch(url)
      .then(response => response.json())
      .then(data => {
        const name = data.display_name || 'Unknown location';
        locationInput.value = name;
      })
      .catch(error => {
        console.error('Geocoding error:', error);
        locationInput.value = '';
      });
  }
 
  let btn = document.querySelector('button[type="submit"]');
  btn.addEventListener("click",()=>{
    window.location.href = "";
  })

document.querySelector("form").addEventListener("submit", () => {
    document.getElementById("submitText").classList.add("d-none");
    document.getElementById("loadingSpinner").classList.remove("d-none");
});
flatpickr(".datetimepicker", {
    enableTime: true,
    dateFormat: "Y-m-d H:i",
});