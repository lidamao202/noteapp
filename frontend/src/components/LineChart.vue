<template>
    <div>
      <canvas ref="chart"></canvas>
    </div>
  </template>
  
  <script>
  import { Chart, registerables } from "chart.js";
  
  export default {
    props: {
      chartData: {
        type: Object,
        required: true,
      },
      chartOptions: {
        type: Object,
        default: () => ({}),
      },
    },
    mounted() {
      Chart.register(...registerables);
      this.renderChart();
    },
    methods: {
      renderChart() {
        new Chart(this.$refs.chart, {
          type: "line", // Change to "bar", "pie", etc., for different chart types
          data: this.chartData,
          options: this.chartOptions,
        });
      },
    },
  };
  </script>
  