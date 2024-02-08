import { UserRoles } from '@/models/user-roles'
import { createContext, useState } from 'react'

interface IAppContextProps {
  children: React.ReactNode
}

interface User {
  name: string
  email: string
  role: UserRoles
}

interface IAppContext {
  useMock: boolean
  user: User
  token: string
  toggleUseMock: () => void
  userHandler: (user: User) => void
  tokenHandler: (token: string) => void
}

let useMock = false

export const AppContext = createContext({} as IAppContext)

export function AppContextProvider({ children }: IAppContextProps) {
  const [user, setUser] = useState<User>({} as User)
  const [token, setToken] = useState<string>({} as string)

  function toggleUseMock() {
    useMock = !useMock
  }

  function userHandler(user: User) {
    setUser(user)
  }

  function tokenHandler(token: string) {
    setToken(token)
  }

  return (
    <AppContext.Provider
      value={{ useMock, user, token, userHandler, tokenHandler, toggleUseMock }}
    >
      {children}
    </AppContext.Provider>
  )
}
