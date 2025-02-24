

import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom"
import { AuthProvider } from "./contexts/AuthContext"
import ProtectedRoute from "./contexts/ProtectedRoute"
import Login from "./pages/Login/index"
import DashboardLayout from "./layouts/dashboardLayout"
import Dashboard from "./pages/Dashboard/index"
import Settings from "./pages/Settings/index"
import Client from "./pages/Client"
import User from "./pages/User"
import Users from "./pages/User"





function App() {
  return (

      <AuthProvider>
        <Routes> 
          <Route path="/" element={<Login />} /> 
          <Route
            path="/dashboard"
            element={
              <ProtectedRoute>
                <DashboardLayout />
              </ProtectedRoute>
            }
          >
            <Route index element={<Dashboard />} />
            <Route path="settings" element={<Settings />} />
            <Route path="client" element={<Client />} />
            <Route path="users" element={<Users />} />
            
          </Route>
        </Routes>
      </AuthProvider>

    
  )
}

export default App

