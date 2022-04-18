$(document).ready(function () {
   const token = $('#token').val()

   const universities = (function () {
      var data = null

      $.ajax({
         async: false,
         type: 'GET',
         url: 'https://localhost:5001/api/universities',
         headers: {
            Authorization: `Bearer ${token}`,
         },
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
      url: '../account/getmaster/',
      // url: 'https://localhost:5001/api/accounts/master',
      // headers: {
      //    Authorization: `Bearer ${token}`,
      // },
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
            toolbar: {
               show: true,
               offsetX: 70,
               offsetY: 0,
               tools: {
                  download: true,
                  selection: true,
                  zoom: true,
                  zoomin: true,
                  zoomout: true,
                  pan: true,
                  reset:
                     true | '<img src="/static/icons/reset.png" width="20">',
                  customIcons: [],
               },
               export: {
                  csv: {
                     filename: undefined,
                     columnDelimiter: ',',
                     headerCategory: 'category',
                     headerValue: 'value',
                     dateFormatter(timestamp) {
                        return new Date(timestamp).toDateString()
                     },
                  },
                  svg: {
                     filename: undefined,
                  },
                  png: {
                     filename: undefined,
                  },
               },
               autoSelected: 'zoom',
            },
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
