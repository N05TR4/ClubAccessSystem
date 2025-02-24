"use client"

import { useState } from "react"
import { Outlet, useNavigate } from "react-router-dom"
import { useAuth } from "../contexts/AuthContext"
import {
  HomeIcon,
  Cog6ToothIcon,
  ArrowLeftOnRectangleIcon,
  Bars3Icon,
  UserGroupIcon,
  UserCircleIcon,
  PresentationChartLineIcon,
  
} from "@heroicons/react/24/outline"

export default function DashboardLayout() {
  const [sidebarOpen, setSidebarOpen] = useState(false)
  const { logout } = useAuth()
  const navigate = useNavigate()

  const handleLogout = () => {
    logout()
    navigate("/")
  }

  return (
    <div className="flex h-screen bg-gray-100">
      {/* Sidebar */}
      <div
        className={`fixed inset-y-0 left-0 z-50 bg-white shadow-lg w-64 transform transition-all duration-300 ease-in-out ${
          sidebarOpen ? "translate-x-0" : "-translate-x-full"
        } md:translate-x-0 md:relative`}
      >
        <div className="h-full flex flex-col">
          {/* Sidebar Header */}
          <div className="px-6 py-4 border-b">
            <h2 className="text-xl font-semibold text-gray-800">CLUB APP</h2>
          </div>

          {/* Sidebar Navigation */}
          <nav className="flex-1 px-6 py-4 space-y-3">
            <button
              onClick={() => navigate("/dashboard")}
              className="flex items-center w-full px-4 py-2 text-gray-700 rounded-lg hover:bg-gray-200 transition duration-200"
            >
              <HomeIcon className="w-5 h-5 mr-3 text-indigo-600" />
              Inicio
            </button>
            <button
              onClick={() => navigate("/dashboard/client")}
              className="flex items-center w-full px-4 py-2 text-gray-700 rounded-lg hover:bg-gray-200 transition duration-200"
            >
              <UserGroupIcon className="w-5 h-5 mr-3 text-indigo-600" />
              Clientes
            </button>
            <button
              onClick={() => navigate("/dashboard/users")}
              className="flex items-center w-full px-4 py-2 text-gray-700 rounded-lg hover:bg-gray-200 transition duration-200"
            >
              <UserCircleIcon className="w-5 h-5 mr-3 text-indigo-600" />
              Usuarios
            </button>
            {/* <button
              onClick={() => navigate("/dashboard/access")}
              className="flex items-center w-full px-4 py-2 text-gray-700 rounded-lg hover:bg-gray-200 transition duration-200"
            >
              <PresentationChartLineIcon className="w-5 h-5 mr-3 text-indigo-600" />
              Registro de Accesos
            </button> */}
            <button
              onClick={() => navigate("/dashboard/settings")}
              className="flex items-center w-full px-4 py-2 text-gray-700 rounded-lg hover:bg-gray-200 transition duration-200"
            >
              <Cog6ToothIcon className="w-5 h-5 mr-3 text-indigo-600" />
              Configuración
            </button>
          </nav>

          {/* Sidebar Footer */}
          <div className="border-t p-4">
            <button
              onClick={handleLogout}
              className="flex items-center w-full px-4 py-2 text-gray-700 rounded-lg hover:bg-gray-200 transition duration-200"
            >
              <ArrowLeftOnRectangleIcon className="w-5 h-5 mr-3 text-red-600" />
              Cerrar Sesión
            </button>
          </div>
        </div>
      </div>

      {/* Main Content */}
      <div className="flex-1 flex flex-col overflow-hidden">
        {/* Header */}
        <header className="bg-white shadow px-6 py-4 flex items-center">
          <button
            onClick={() => setSidebarOpen(!sidebarOpen)}
            className="md:hidden mr-4 focus:outline-none focus:ring-2 focus:ring-indigo-500"
          >
            <Bars3Icon className="w-6 h-6 text-gray-700" />
          </button>
          <h1 className="text-lg font-semibold text-gray-900">Dashboard</h1>
        </header>
        {/* Main Content */}
        <main className="flex-1 overflow-auto p-6">
          <Outlet />
          
        </main>
      </div>
    </div>
  )
}
