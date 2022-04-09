﻿$(document).ready(function () {
   $('.table-employee').DataTable({
      filter: true,
      orderMulti: false,
      // dom: 'Blfrtip',
      // dom: "<'row justify-content-between'<'col-md-3'l><'d-flex align-items-center justify-content-center'<'mr-3'B><'mt-1'f>>>",
      // columnDefs: [{ type: 'natural', targets: 4 }],
      // order: [[4, 'desc']],
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
               // return parseInt(data['salary'])
               return convertToRupiah(parseInt(data['salary']))
               // return `Rp. ${data['salary']}`
            },
         },
         {
            data: null,
            orderable: false,
            render: (data, type, row, meta) => {
               return `
               <div class="btn-action d-flex justify-content-around align-items-center">
                  <button class="btn btn-outline-warning" onClick="openUpdateModal('${data['nik']}')"><i class="fa-solid fa-pen-to-square"></i></button>
                  <button class="btn btn-outline-danger" onClick="Delete('${data['nik']}')"><i class="fa-solid fa-trash"></i></button>
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
      $('#university_id_update').html(renderUniversities)
   })
   .fail((e) => {
      console.error(e)
   })

const Delete = (nik) => {
   swal({
      title: 'Are you sure?',
      text: 'Once deleted, you will not be able to recover this data!',
      icon: 'warning',
      buttons: true,
      dangerMode: true,
   }).then((willDelete) => {
      if (willDelete) {
         $.ajax({
            url: `https://localhost:44348/api/accounts/remove/${nik}`,
            type: 'DELETE',
            headers: {
               Accept: 'application/json',
               'Content-Type': 'application/json',
            },
         })
            .done((result) => {
               $('.table-employee').DataTable().ajax.reload()
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

const openUpdateModal = (nik) => {
   $('#updateModal').modal('toggle')
   // $('#employee_form').trigger('reset')

   $.ajax({
      type: 'GET',
      url: `https://localhost:44348/api/accounts/master/${nik}`,
   })
      .done((response) => {
         const data = response.result
         const [first_name, ...last_name] = data.fullName.split(' ')
         let gender

         data.gender === 'Laki-laki' ? (gender = 0) : (gender = 1)

         $('#nik').val(nik)
         $('#education_id_update').val(data.education_id)
         $('#first_name_update').val(first_name)
         $('#last_name_update').val(last_name.join(' '))
         $('#phone_update').val(data.phone)
         $('#birth_date_update').val(formatDate(data.birthDate))
         $('#gender_update').val(gender)
         $('#salary_update').val(data.salary)
         $('#email_update').val(data.email)
         $('#degree_update').val(data.degree)
         $('#gpa_update').val(data.gpa)
         $('#university_id_update').val(data.universityId)
      })
      .fail((e) => {
         console.error(e)
      })
}

const Update = () => {
   event.preventDefault()
   //  "NIK": "2022001",
   //     "FirstName": "Rama",
   //     "LastName": "Dhani",
   //     "phone": "0882277344",
   //     "BirthDate": "2000-03-23",
   //     "Gender": 0,
   //     "Salary": 1800000,
   //     "Email": "ramadhani@gmail.com",
   //     "education_id": 6,
   //     "Degree": "S1",
   //     "GPA": "3.5",
   //     "UniversityId": 3

   const data = {}
   data.nik = $('#nik').val()
   data.FirstName = $('#first_name_update').val()
   data.LastName = $('#last_name_update').val()
   data.Phone = $('#phone_update').val()
   data.BirthDate = $('#birth_date_update').val()
   data.Gender = parseInt($('#gender_update').val())
   data.Salary = parseInt($('#salary_update').val())
   data.Email = $('#email_update').val()
   data.Education_id = parseInt($('#education_id_update').val())
   data.Degree = $('#degree_update').val()
   data.GPA = $('#gpa_update').val()
   data.UniversityId = parseInt($('#university_id_update').val())

   $.ajax({
      url: 'https://localhost:44348/api/accounts/master/update',
      type: 'PUT',
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

function formatDate(date) {
   var d = new Date(date),
      month = '' + (d.getMonth() + 1),
      day = '' + d.getDate(),
      year = d.getFullYear()

   if (month.length < 2) month = '0' + month
   if (day.length < 2) day = '0' + day

   return [year, month, day].join('-')
}

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
