import DashboardLayout from "@/layout/dashboard/DashboardLayout.vue";
// GeneralViews
import NotFound from "@/pages/NotFoundPage.vue";

// Admin pages
import Dashboard from "@/pages/Dashboard.vue";
import Statement from "@/pages/Statement.vue";
import Transfer from "@/pages/Transfer.vue";
import Deposit from "@/pages/Deposit.vue";
import Payment from "@/pages/Payment.vue";
import Withdraw from "@/pages/Withdraw.vue";
import MyAccount from "@/pages/MyAccount.vue";

const routes = [
  {
    path: "/",
    component: DashboardLayout,
    redirect: "/dashboard",
    children: [
      {
        path: "dashboard",
        name: "dashboard",
        component: Dashboard
      },
      {
        path: "account",
        name: "account",
        component: MyAccount
      },
      {
        path: "statement",
        name: "statement",
        component: Statement
      },
      {
        path: "payment",
        name: "payment",
        component: Payment
      },
      {
        path: "transfer",
        name: "transfer",
        component: Transfer
      },
      {
        path: "deposit",
        name: "deposit",
        component: Deposit
      },
      {
        path: "withdraw",
        name: "withdraw",
        component: Withdraw
      }
    ]
  },
  { path: "*", component: NotFound }
];

/**
 * Asynchronously load view (Webpack Lazy loading compatible)
 * The specified component must be inside the Views folder
 * @param  {string} name  the filename (basename) of the view to load.
function view(name) {
   var res= require('../components/Dashboard/Views/' + name + '.vue');
   return res;
};**/

export default routes;
