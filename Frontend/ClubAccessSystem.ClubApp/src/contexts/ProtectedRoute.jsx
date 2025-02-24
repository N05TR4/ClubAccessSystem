import { Navigate } from "react-router-dom"
import { useAuth } from "./AuthContext"

// export default function ProtectedRoute({ children }) {
//   const { isAuthenticated, isLoading } = useAuth()
  
//   if (isLoading) {
//     return <div className="flex items-center justify-center h-screen">Cargando...</div>
//   }

//   if (!isAuthenticated) {
//     return <Navigate to="/" />
//   }

//   return children
// }

export default function ProtectedRoute({ children }) {
  const { isAuthenticated, isLoading } = useAuth()
  
  if (isLoading) {
    return <div className="flex items-center justify-center h-screen">Cargando...</div>
  }

  return isAuthenticated ? children : <Navigate to="/" />
}