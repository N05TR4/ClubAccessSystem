import { createContext, useContext, useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import api from "../data/api"

const AuthContext = createContext()

export function AuthProvider({ children }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false)
  const [isLoading, setIsLoading] = useState(true)
  const [user, setUser] = useState(null)
  const navigate = useNavigate()

  // Verificar token al cargar la aplicación
  useEffect(() => {
    const verifyToken = async () => {
      const token = localStorage.getItem("token")
      
      if (!token) {
        setIsLoading(false)
        return
      }

      try {
        const response = await api.get("/Auth/verify")
        setUser(response.data.user)
        setIsAuthenticated(true)
      } catch (error) {
        console.error("Error verificando token:", error)
        localStorage.removeItem("token")
        setIsAuthenticated(false)
        setUser(null)
      } finally {
        setIsLoading(false)
      }
    }

    verifyToken()
  }, [])

  const login = async (email, password) => {
    try {
      const response = await api.post("/Auth/login", { email, password })
      const { token, user } = response.data

      localStorage.setItem("token", token)
      setIsAuthenticated(true)
      setUser(user)

      navigate("/dashboard")
    } catch (error) {
      console.error("Error en el inicio de sesión:", error.response?.data || error.message)
      throw error
    }
  }

  const logout = () => {
    localStorage.removeItem("token")
    setIsAuthenticated(false)
    setUser(null)
    navigate("/")
  }

  if (isLoading) {
    return <div className="flex items-center justify-center h-screen">Cargando...</div>
  }

  return (
    <AuthContext.Provider 
      value={{ 
        isAuthenticated, 
        login, 
        logout, 
        user,
        isLoading 
      }}
    >
      {children}
    </AuthContext.Provider>
  )
}

export function useAuth() {
  const context = useContext(AuthContext)
  if (!context) {
    throw new Error("useAuth debe usarse dentro de un AuthProvider")
  }
  return context
}