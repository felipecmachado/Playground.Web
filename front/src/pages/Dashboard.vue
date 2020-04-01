<template>
  <div>

    <!--Stats cards-->
    <div class="row">
      <div class="col-md-6 col-xl-3" :key="account.title">
        <stats-card>
          <div class="icon-big text-center icon-success" slot="header">
            <i class="ti-wallet"></i>
          </div>
          <div class="numbers" slot="content">
            <p>Funds</p>
            $ {{account.balance}}
          </div>
          <div class="stats" slot="footer">
            <i class="ti-calendar"></i> Now
          </div>
        </stats-card>
      </div>
    </div>

    <!--Charts-->
    <div class="row">

      <div class="col-12">
        <chart-card title="Daily Balance"
                    sub-title="Keep track of your wallet"
                    :chart-data="balancesChart.data"
                    :chart-options="balancesChart.options"
                    :key="balancesChart.title">
          <div slot="legend">
            <i class="fa fa-circle text-info"></i> Funds
          </div>
        </chart-card>
      </div>

    </div>

  </div>
</template>
<script>
import axios from 'axios'
import { StatsCard, ChartCard } from "@/components/index";
import Chartist from 'chartist';
export default {
  created() {

    axios
      .get('http://localhost:5000/api/accounts/1', { headers: { Authorization: localStorage.getItem('token') }})
      .then(response => (this.account = response.data));
      
    axios
      .get('http://localhost:5000/api/accounts/1/balances', { headers: { Authorization: localStorage.getItem('token') }})
      .then(response => 
      {
        this.balances = response.data;
        this.balancesChart.data.labels = this.balances.map(function(b){ return b.timestamp.substring(0,10);});
        this.balancesChart.data.series = [ this.balances.map(function(b){ return b.amount;})];
      });

      this.$forceUpdate();
  },
  components: {
    StatsCard,
    ChartCard
  },
  data() {
    return {
      account: 
      {
        balance: 0
      },
      balances: null,      
      balancesChart: {
        title: "title",
        data: {
          labels: null,
          series: null
        },
        options: {
          low: 0,
          high: 400,
          showArea: true,
          height: "245px",
          axisX: {
            showGrid: false
          },
          lineSmooth: Chartist.Interpolation.simple({
            divisor: 3
          }),
          showLine: true,
          showPoint: false
        }
      }
    };
  }
};
</script>
<style>
</style>
