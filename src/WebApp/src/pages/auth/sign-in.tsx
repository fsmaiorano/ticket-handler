import { useMutation } from '@tanstack/react-query'
import { Helmet } from 'react-helmet-async'
import { useForm } from 'react-hook-form'
import { Link, useNavigate } from 'react-router-dom'
import { toast } from 'sonner'
import { z } from 'zod'

import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { AppContext } from '@/contexts/app-context'
import { signIn } from '@/services/sign-in'
import { useContext } from 'react'

const signInForm = z.object({
  email: z.string().email(),
  password: z.string(),
})

type SignInForm = z.infer<typeof signInForm>

export function SignIn() {
  const navigate = useNavigate()
  // const [searchParams] = useSearchParams()
  const { userHandler, tokenHandler, user } = useContext(AppContext)

  const { handleSubmit, register, formState } = useForm<SignInForm>({
    defaultValues: {
      // email: searchParams.get('email') ?? '',
      // password: searchParams.get('password') ?? '',
    },
  })

  const { mutateAsync: authenticate } = useMutation({ mutationFn: signIn })

  if (user.id) {
    navigate('/')
  }

  async function handleSignIn(data: SignInForm) {
    try {
      const response = await authenticate({
        email: data.email,
        password: data.password,
      })

      if (response) {
        userHandler(response.user)
        tokenHandler(response.token)
        navigate(response.redirectUrl, {
          replace: true,
          state: { from: '/' },
        })

        // window.location.href = response.redirectUrl
      } else {
        toast.success('We send you an email with a link to sign in')
      }
    } catch {
      toast.error('Something went wrong')
    }
  }

  return (
    <>
      <Helmet title="Sign-in" />
      <div className="p-8">
        <Button asChild className="absolute right-4 top-8" variant={'outline'}>
          <Link to="/sign-up">New Holder</Link>
        </Button>
        <div className="flex w-[320px] flex-col justify-center gap-6">
          <div className="flex flex-col gap-2 text-center">
            <h1 className="text-2xl font-semibold tracking-tight">Dashboard</h1>
            <p className="text-sm text-muted-foreground">
              Sign in to your account
            </p>
          </div>
          <form onSubmit={handleSubmit(handleSignIn)} className="space-y-4">
            <div className="space-y-2">
              <Label htmlFor="email">Email</Label>
              <Input id="email" type="email" {...register('email')} />
            </div>
            <div className="space-y-2">
              <Label htmlFor="email">Password</Label>
              <Input id="password" type="password" {...register('password')} />
            </div>
            <Button
              disabled={formState.isSubmitting}
              type="submit"
              className="w-full"
            >
              Sign in
            </Button>
          </form>
        </div>
      </div>
    </>
  )
}
