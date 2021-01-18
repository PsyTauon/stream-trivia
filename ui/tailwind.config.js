const colors = require('tailwindcss/colors')

module.exports = {
  purge: [],
  darkMode: false, // or 'media' or 'class'
  theme: {
    darkMode: 'media',
    screens: {
      sm: '480px',
      md: '768px',
      lg: '976px',
      xl: '1440px',
    },
    fontFamily: {
      sans: ['Graphik', 'sans-serif'],
      serif: ['Merriweather', 'serif'],
    },
    extend: {
      colors: {
        primary: '#074DD6',
        'primary-dark': '#042C7A',
        'primary-medium': '#1A5DE1',
        'primary-light': '#4679DF',
        secondary: '#3409D9',
        'secondary-light': '#6A4AE1',
        neutral: '#00CFC0',
      },
      fontFamily: {
        'title': ['Racing Sans One'],
        'subtitle': ['Srisakdi'],
      },
      spacing: {
        '128': '32rem',
        '144': '36rem',
      },
      borderRadius: {
        '4xl': '2rem',
      }
    }
  },
  variants: {
    extend: {},
  },
  plugins: [],
}