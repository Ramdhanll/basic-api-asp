$(document).ready(function () {
   $('.table-university').DataTable({
      filter: true,
      orderMulti: false,
      dom:
         "<'row justify-content-between align-items-center'<'col-md-3'l><'d-flex align-items-center justify-content-center'<'mr-3'B><'mt-1'f>>>" +
         "<'row'<'col-md-12'tr>>" +
         "<'row'<'col-md-5'i><'col-md-7'p>>",
      buttons: [
         {
            extend: 'excel',
            text: 'Excel',
            titleAttr: 'Excel',
            exportOptions: {
               columns: [0, 1, 2, 3, 4, 5],
            },
         },
         {
            extend: 'pdfHtml5',
            text: 'PDF',
            titleAttr: 'PDF',
            orientation: 'portrait',
            pageSize: 'A4',
            exportOptions: {
               columns: [0, 1, 2, 3, 4, 5],
            },
         },
      ],
      ajax: {
         url: '../university/getall',
         datatype: 'json',
         dataSrc: 'result',
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
            orderable: false,
            width: '100px',
            render: (data, type, row, meta) => {
               return `
               <div class="btn-action d-flex justify-content-around align-items-center">
                  <button class="btn btn-outline-warning" onClick="openUpdateModal('${data['id']}')"><i class="fa-solid fa-pen-to-square"></i></button>
                  <button class="btn btn-outline-danger" onClick="Delete('${data['id']}')"><i class="fa-solid fa-trash"></i></button>
               </div>
               `
            },
         },
      ],
   })
})

const convertPhoneNumber = ([first, ...rest]) => {
   if (first == '0') return '+62 ' + rest.join('')

   return first + rest.join('')
}

function convertToRupiah(angka) {
   var rupiah = ''
   var angkarev = angka.toString().split('').reverse().join('')
   for (var i = 0; i < angkarev.length; i++)
      if (i % 3 == 0) rupiah += angkarev.substr(i, 3) + '.'
   return (
      'Rp. ' +
      rupiah
         .split('', rupiah.length - 1)
         .reverse()
         .join('')
   )
}

// Example starter JavaScript for disabling form submissions if there are invalid fields

const Insert = () => {
   event.preventDefault()
   const data = {}

   data.Name = $('#name').val()

   $.ajax({
      url: 'https://localhost:5001/api/universities',
      type: 'POST',
      headers: {
         Accept: 'application/json',
         'Content-Type': 'application/json',
      },
      dataType: 'json',
      data: JSON.stringify(data),
   })
      .done((result) => {
         $('.table-university').DataTable().ajax.reload()
         $('#university_form').trigger('reset')
         $('#addModal').modal('toggle')
         swal('Success!', 'Data berhasil diregistrasi!', 'success')
         //buat alert pemberitahuan jika success
      })
      .fail((error) => {
         console.log('error', error.responseJSON.message)
         swal('error!', error.responseJSON.message, 'error')
         //alert pemberitahuan jika gagal
      })
}

// show hide password
$(document).ready(function () {
   $('#show_hide_password button').on('click', function (event) {
      event.preventDefault()
      if ($('#show_hide_password input').attr('type') == 'text') {
         $('#show_hide_password input').attr('type', 'password')
         $('#show_hide_password i').addClass('fa-eye-slash')
         $('#show_hide_password i').removeClass('fa-eye')
      } else if ($('#show_hide_password input').attr('type') == 'password') {
         $('#show_hide_password input').attr('type', 'text')
         $('#show_hide_password i').removeClass('fa-eye-slash')
         $('#show_hide_password i').addClass('fa-eye')
      }
   })
})

const Delete = (id) => {
   swal({
      title: 'Are you sure?',
      text: 'Once deleted, you will not be able to recover this data!',
      icon: 'warning',
      buttons: true,
      dangerMode: true,
   }).then((willDelete) => {
      if (willDelete) {
         $.ajax({
            url: `https://localhost:5001/api/universities/${id}`,
            type: 'DELETE',
            headers: {
               Accept: 'application/json',
               'Content-Type': 'application/json',
            },
         })
            .done((result) => {
               $('.table-university').DataTable().ajax.reload()
               swal('Poof! Your data has been deleted!', {
                  icon: 'success',
               })
            })
            .fail((error) => {
               console.log('error', error.responseJSON.message)
               swal('Success!', error.responseJSON.message, 'error')
            })
      }
   })
}

const openUpdateModal = (id) => {
   $('#updateModal').modal('toggle')
   // $('#university_form').trigger('reset')

   $.ajax({
      type: 'GET',
      url: `https://localhost:5001/api/universities/${id}`,
   })
      .done((response) => {
         const data = response.result

         $('#id').val(data.id)
         $('#name_update').val(data.name)
      })
      .fail((e) => {
         console.error(e)
      })
}

const Update = () => {
   event.preventDefault()

   //    {
   //       "id": 2,
   //       "name": "Universitas D",
   //       "education": null
   //   }
   const data = {}
   data.id = $('#id').val()
   data.Name = $('#name_update').val()

   $.ajax({
      url: 'https://localhost:5001/api/universities',
      type: 'PUT',
      headers: {
         Accept: 'application/json',
         'Content-Type': 'application/json',
      },
      dataType: 'json',
      data: JSON.stringify(data),
   })
      .done((result) => {
         $('.table-university').DataTable().ajax.reload()
         $('#university_form').trigger('reset')
         $('#updateModal').modal('toggle')
         swal('Success!', 'Data berhasil diupdate!', 'success')
         //buat alert pemberitahuan jika success
      })
      .fail((error) => {
         console.log('error', error.responseJSON.message)
         swal('error!', error.responseJSON.message, 'error')
         //alert pemberitahuan jika gagal
      })
}
