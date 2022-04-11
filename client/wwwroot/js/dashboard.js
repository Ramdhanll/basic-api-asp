$(document).ready(function () {
   const universities = (function () {
      var data = null

      $.ajax({
         async: false,
         type: 'GET',
         url: 'https://localhost:5001/api/universities',
         data: {},
         success: function (response) {
            data = response.result
         },
      })
      return data
   })()

   // GET EMPLOYEES
   $.ajax({
      type: 'GET',
      url: 'https://localhost:5001/api/accounts/master',
      data: {},
   })
      .done((result) => {
         console.log(result.result)
         chartGender(result.result)
         chartUniversity(result.result)
      })
      .fail((e) => {
         console.error(e)
      })

   const chartGender = (employees) => {
      const male = employees.filter(
         (employee) => employee.gender === 'Laki-laki'
      )
      const female = employees.filter(
         (employee) => employee.gender === 'Perempuan'
      )

      var options = {
         series: [male.length, female.length],
         chart: {
            width: 380,
            type: 'pie',
         },
         labels: ['Male', 'Female'],
         responsive: [
            {
               breakpoint: 480,
               options: {
                  chart: {
                     width: 200,
                  },
                  legend: {
                     position: 'bottom',
                  },
               },
            },
         ],
      }

      var chart = new ApexCharts(
         document.querySelector('#chart-gender'),
         options
      )
      chart.render()
   }

   const chartUniversity = (employees) => {
      const chartUniversity = universities.map((u) => {
         let count = 0

         if (employees !== undefined) {
            employees.forEach((emp) => {
               if (u.name === emp.universityName) {
                  count += 1
               }
            })
         }

         return { name: u.name, empNum: count }
      })

      var options = {
         series: [
            {
               data: chartUniversity.map((university) => university.empNum),
            },
         ],
         chart: {
            type: 'bar',
            height: 350,
         },
         plotOptions: {
            bar: {
               borderRadius: 4,
               horizontal: true,
            },
         },
         dataLabels: {
            enabled: false,
         },
         xaxis: {
            categories: chartUniversity.map((university) => university.name),
         },
      }

      var chart = new ApexCharts(
         document.querySelector('#chart-university'),
         options
      )
      chart.render()
   }
})
