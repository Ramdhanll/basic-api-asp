$.ajax({
   url: 'https://pokeapi.co/api/v2/pokemon?limit=100&offset=200',
   success: (result) => {
      let tbody = ''

      $.each(result.results, (key, val) => {
         tbody += `
            <tr>
               <td scope="row">${key + 1}</td>
               <td>${capitalize(val.name)}</td>
               <td>
                     <button class="btn btn-primary" onClick="detailPokemon('${
                        val.url
                     }')" data-toggle="modal" data-target="#exampleModalCenter">Detail</button>
               </td>
            </tr>
         `
      })
      $('#tbody').html(tbody)
   },
})

const detailPokemon = (url) => {
   $.ajax({
      url: url,
      success: (result) => {
         // name pokemon
         $('.species-name').html(capitalize(result.name))

         // types pokemon
         let types = ''
         $.each(result.types, (key, value) => {
            types += `<span class="badge ${colorBadge(
               value.type.name
            )} mr-2 px-2 py-1">${value.type.name}</span>`
         })
         $('.types').html(types)

         // image
         $('.img-pokemon').attr(
            'src',
            `${result.sprites.other['official-artwork'].front_default}`
         )

         // species
         $('.species').html(capitalize(result.species.name))

         // height
         $('.height').html(`${result.height} Inch`)

         // weight
         $('.weight').html(`${result.weight} lbs`)

         // abilities
         let abilities = []
         $.each(result.abilities, (key, value) => {
            abilities.push(value.ability.name)
         })
         $('.abilities').html(abilities.join(', '))

         // HP
         const hp = result.stats[0].base_stat
         $('.hp').html(hp)
         $('.progress-hp').html(
            `<div class="progress mt-1">
           <div class="progress-bar ${colorProgress(
              hp
           )}" role="progressbar" style="width: ${hp}%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
         </div>`
         )

         // Attack
         const attack = result.stats[1].base_stat
         $('.attack').html(attack)
         $('.progress-attack').html(
            `<div class="progress mt-1">
           <div class="progress-bar ${colorProgress(
              attack
           )}" role="progressbar" style="width: ${attack}%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
         </div>`
         )

         // Defense
         const defense = result.stats[2].base_stat
         $('.defense').html(defense)
         $('.progress-defense').html(
            `<div class="progress mt-1">
           <div class="progress-bar ${colorProgress(
              defense
           )}" role="progressbar" style="width: ${defense}%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
         </div>`
         )

         // Sp. Attack
         const spAtk = result.stats[3].base_stat
         $('.sp-atk').html(spAtk)
         $('.progress-sp-atk').html(
            `<div class="progress mt-1">
           <div class="progress-bar ${colorProgress(
              spAtk
           )}" role="progressbar" style="width: ${spAtk}%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
         </div>`
         )

         // Sp. Defense
         const spDefense = result.stats[4].base_stat
         $('.sp-def').html(spDefense)
         $('.progress-sp-def').html(
            `<div class="progress mt-1">
           <div class="progress-bar ${colorProgress(
              spDefense
           )}" role="progressbar" style="width: ${spDefense}%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
         </div>`
         )

         // Speed
         const speed = result.stats[5].base_stat
         $('.speed').html(speed)
         $('.progress-speed').html(
            `<div class="progress mt-1">
           <div class="progress-bar ${colorProgress(
              speed
           )}" role="progressbar" style="width: ${speed}%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
         </div>`
         )

         // total
         const total = result.stats.reduce((a, b) => a + b.base_stat, 0)
         const percentTotal = (total / 600) * 100

         console.log('total', percentTotal)
         $('.total').html(total)
         $('.progress-total').html(
            `<div class="progress mt-1">
           <div class="progress-bar ${colorProgress(
              percentTotal
           )}" role="progressbar" style="width: ${percentTotal}%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="600"></div>
         </div>`
         )

         console.log(result)
      },
   })
}

const capitalize = ([first, ...rest]) => first.toUpperCase() + rest.join('')

const colorProgress = (percent) => {
   if (percent <= 40) {
      return 'bg-danger'
   } else if (percent > 40 && percent < 70) {
      return 'bg-warning'
   } else if (percent >= 70) {
      return 'bg-success'
   }
}

const colorBadge = (type) => {
   if (type == 'fire') {
      return 'badge-danger'
   } else if (type == 'water') {
      return 'badge-primary'
   } else if (type == 'grass') {
      return 'badge-success'
   } else if (type == 'poison') {
      return 'badge-warning'
   } else if (type == 'bug') {
      return 'badge-secondary'
   } else if (type == 'flying') {
      return 'badge-orange'
   } else if (type == 'normal') {
      return 'badge-normal'
   } else if (type == 'ground') {
      return 'badge-ground'
   } else if (type == 'dark') {
      return 'badge-dark'
   } else if (type == 'fairy') {
      return 'badge-fairy'
   } else {
      return 'badge-info'
   }
}

$(document).ready(function () {
   $('.table-pokemon').DataTable()

   $('.table-pokemon2').DataTable({
      filter: true,
      orderMulti: false,
      ajax: {
         url: 'https://pokeapi.co/api/v2/pokemon?limit=10',
         datatype: 'json',
         dataSrc: 'results',
      },
      columns: [
         {
            data: null,
            name: 'no',
            autoWidth: true,
            render: function (data, type, row, meta) {
               return meta.row + 1
            },
         },
         {
            data: 'name',
         },
         {
            data: null,
            render: function (data, type, row) {
               //render berfungsi utk membuat column bisa kita manipulasi string nya
               return `
               <button class="btn btn-primary" onClick="detailPokemon('${data.url}')" data-toggle="modal" data-target="#exampleModalCenter">Detail</button>
               `
            },
            autoWidth: true,
         },
      ],
   })
})
