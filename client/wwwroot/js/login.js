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

const handleLogin = () => {
   event.preventDefault()

   const email = $('#email').val()
   const password = $('#password').val()

   $.ajax({
      type: 'POST',
      url: '../login/auth',
      data: JSON.stringify({ email, password }),
      headers: {
         Accept: 'application/json',
         'Content-Type': 'application/json',
      },
      dataType: 'json',
      success: function (response) {
         console.log(response)
      },
   })
}

// const handleLogin = () => {
//    event.preventDefault()

//    const email = $('#email').val()
//    const password = $('#password').val()

//    $.ajax({
//       type: 'POST',
//       url: 'https://localhost:5001/api/accounts/login',
//       data: JSON.stringify({ email, password }),
//       headers: {
//          Accept: 'application/json',
//          'Content-Type': 'application/json',
//       },
//       dataType: 'json',
//       success: function (response) {
//          console.log(response)
//       },
//    })
// }
