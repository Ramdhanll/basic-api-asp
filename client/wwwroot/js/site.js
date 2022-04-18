const animals = [
   { name: 'garfield', species: 'cat', class: { name: 'mamalia' } },
   { name: 'nemo', species: 'fish', class: { name: 'invertebrata' } },
   { name: 'tom', species: 'cat', class: { name: 'mamalia' } },
   { name: 'garry', species: 'cat', class: { name: 'mamalia' } },
   { name: 'dory', species: 'fish', class: { name: 'invertebrata' } },
   { name: 'junet', species: 'fish', class: { name: 'invertebrata' } },
   { name: 'kimo', species: 'cat', class: { name: 'mamalia' } },
   { name: 'dora', species: 'cat', class: { name: 'mamalia' } },
   { name: 'rahel', species: 'fish', class: { name: 'invertebrata' } },
]

const onlyCat = animals.filter((animal) => animal.species === 'cat')

animals.forEach((animal) => {
   if (animal.species == 'fish') animal.class.name = 'Non-Mamalia'
   return animal
})
