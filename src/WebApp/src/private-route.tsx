import { useContext } from 'react'
import { Navigate, useLocation } from 'react-router-dom'
import { AppContext } from './contexts/app-context'

interface PrivateRouteProps {
  children: React.ReactNode
}

const PrivateRoute: React.FC<PrivateRouteProps> = ({ children }) => {
  const { user } = useContext(AppContext)
  const location = useLocation()

  return user.id ? children : <Navigate to="/sign-in" state={{ from: location }} />
}

export default PrivateRoute
