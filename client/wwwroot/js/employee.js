$(document).ready(function () {
   $('.table-employee').DataTable({
      filter: true,
      orderMulti: false,
      // dom: 'Blfrtip',
      // dom: "<'row justify-content-between'<'col-md-3'l><'d-flex align-items-center justify-content-center'<'mr-3'B><'mt-1'f>>>",
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
         url: 'https://localhost:44348/api/accounts/master/',
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
            data: 'nik',
         },
         {
            data: 'fullName',
         },
         {
            data: null,
            render: (data) => {
               return convertPhoneNumber(data['phone'])
            },
         },
         {
            data: null,
            render: (data) => {
               return moment(data['birthDate']).format('LL')
            },
         },
         {
            data: null,
            render: (data) => {
               return convertToRupiah(data['salary'])
               // return `Rp. ${data['salary']}`
            },
         },
         {
            data: null,
            orderable: false,
            render: (data, type, row, meta) => {
               return `<button class="btn btn-outline-primary" onClick="detailEmployee('${row.nik}')">Detail</button>`
            },
         },
      ],
   })
})

const detailEmployee = (data) => {
   console.log(data)
}

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
   const data = {}

   data.FirstName = $('#first_name').val()
   data.LastName = $('#last_name').val()
   data.Phone = $('#phone').val()
   data.BirthDate = $('#birth_date').val()
   data.Gender = parseInt($('#gender').val())
   data.Salary = parseInt($('#salary').val())
   data.Email = $('#email').val()
   data.Password = $('#password').val()
   data.Degree = $('#degree').val()
   data.GPA = $('#gpa').val()
   data.UniversityId = parseInt($('#university_id').val())

   $.ajax({
      url: 'https://localhost:44348/api/accounts/register',
      type: 'POST',
      headers: {
         Accept: 'application/json',
         'Content-Type': 'application/json',
      },
      dataType: 'json',
      data: JSON.stringify(data),
   })
      .done((result) => {
         $('.table-employee').DataTable().ajax.reload()
         $('#employee_form').trigger('reset')
         $('#registerModal').modal('toggle')
         swal('Success!', 'Data berhasil diregistrasi!', 'success')
         //buat alert pemberitahuan jika success
      })
      .fail((error) => {
         console.log('error', error.responseJSON.message)
         swal('Success!', error.responseJSON.message, 'error')
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

// get universities and display to modal insert
$.ajax({
   type: 'GET',
   url: 'https://localhost:44348/api/universities',
   data: {},
})
   .done((result) => {
      let renderUniversities =
         '<option selected disabled value="">Choose universities</option>'

      $.each(result.result, (i, val) => {
         renderUniversities += `<option value="${val.id}">${val.name}</option>`
      })

      $('#university_id').html(renderUniversities)
   })
   .fail((e) => {
      console.error(e)
   })

// ;(function () {
//    'use strict'
//    window.addEventListener(
//       'load',
//       function () {
//          // Fetch all the forms we want to apply custom Bootstrap validation styles to
//          var forms = document.getElementsByClassName('needs-validation')
//          // Loop over them and prevent submission
//          var validation = Array.prototype.filter.call(forms, function (form) {
//             form.addEventListener(
//                'submit',
//                function (event) {
//                   if (form.checkValidity() === false) {
//                      event.preventDefault()
//                      event.stopPropagation()
//                   }
//                   form.classList.add('was-validated')
//                },
//                false
//             )
//          })

//          console.log('validation', validation)
//          console.log('forms', forms)
//       },
//       false
//    )
// })()
