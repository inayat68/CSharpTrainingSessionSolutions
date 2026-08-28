import { createRouter, createWebHistory } from "vue-router";

// Pages
import Login from "../pages/Login.vue";
import Register from "../pages/Register.vue";
import Dashboard from "../pages/Dashboard.vue";
import Users from "../pages/Users.vue";
import Tasks from "../pages/Tasks.vue";
import ChangePassword from "../pages/ChangePassword.vue";

import.meta.env.VITE_API_BASE_URL

const routes = [
  {
    path: "/",
    name: "login",
    component: Login,
  },
  {
    path: "/register",
    name: "register",
    component: Register,
  },
  {
    path: "/dashboard",
    name: "dashboard",
    component: Dashboard,
    meta: { auth: true },
  },
  {
    path: "/users",
    name: "users",
    component: Users,
    meta: { auth: true, role: "Admin" },
  },
  {
    path: "/tasks",
    name: "tasks",
    component: Tasks,
    meta: { auth: true },
  },
  {
    path: "/change-password",
    name: "change-password",
    component: ChangePassword,
    meta: { auth: true },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});


// ✅ GLOBAL ROUTE GUARD (FIXED - NO next())
router.beforeEach((to, from) => {
  const token = localStorage.getItem("token");
  const user = JSON.parse(localStorage.getItem("user") || "{}");

  // 🔒 Not logged in
  if (to.meta.auth && !token) {
    return { name: "login" };
  }

  // 🔒 Role-based access
  if (to.meta.role && user.role !== to.meta.role) {
    return { name: "dashboard" };
  }

  return true;
});

export default router;