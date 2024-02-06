import { useMutation } from '@tanstack/react-query'
import { Helmet } from 'react-helmet-async'
import { useForm } from 'react-hook-form'
import { Link, useNavigate } from 'react-router-dom'
import { toast } from 'sonner'
import { z } from 'zod'

import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import { registerHolder } from '@/services/register-holders'

const signUpForm = z.object({
    email: z.string().email(),
    holderName: z.string().min(3),
    fullName: z.string(),
    password: z.string().min(1),
})

type SignUpForm = z.infer<typeof signUpForm>

export function SignUp() {
    const navigate = useNavigate()
    const { handleSubmit, register, formState } = useForm<SignUpForm>()

    const { mutateAsync: registerHolderFn } = useMutation({
        mutationFn: registerHolder,
    })

    async function handleSignUp(data: SignUpForm) {
        try {
            await registerHolderFn({
                email: data.email,
                holderName: data.holderName,
                fullName: data.fullName,
                password: data.password,
            })

            toast.success('Account created successfully', {
                action: {
                    label: 'Sign in',
                    onClick: () => navigate(`/sign-in?email=${data.email}`),
                },
            })
        } catch {
            toast.error('Something went wrong')
        }
    }

    return (
        <>
            <Helmet title="Sign-up" />
            <div className="p-8">
                <Button asChild className="absolute right-4 top-8" variant={'outline'}>
                    <Link to="/sign-in">To sign in</Link>
                </Button>
                <div className="flex w-[320px] flex-col justify-center gap-6">
                    <div className="flex flex-col gap-2 text-center">
                        <h1 className="text-2xl font-semibold tracking-tight">
                            Create an account
                        </h1>
                        <p className="text-sm text-muted-foreground">
                            Sign up to create an account and start using our services
                        </p>
                    </div>
                    <form onSubmit={handleSubmit(handleSignUp)} className="space-y-4">
                        <div className="space-y-2">
                            <Label htmlFor="holderName">Holder name</Label>
                            <Input
                                id="holderName"
                                type="text"
                                {...register('holderName')}
                            />
                        </div>
                        <div className="space-y-2">
                            <Label htmlFor="fullName">Full Name</Label>
                            <Input
                                id="fullName"
                                type="text"
                                {...register('fullName')}
                            />
                        </div>
                        <div className="space-y-2">
                            <Label htmlFor="email">E-mail</Label>
                            <Input id="email" type="text" {...register('email')} />
                        </div>
                        <div className="space-y-2">
                            <Label htmlFor="password">Password</Label>
                            <Input
                                id="password"
                                type="text"
                                {...register('password')}
                            />
                        </div>
                        <Button
                            disabled={formState.isSubmitting}
                            type="submit"
                            className="w-full"
                        >
                            Create account
                        </Button>
                        <p className="px-6 text-center text-sm leading-relaxed text-muted-foreground">
                            By creating an account, you agree to our{' '}
                            <Link to="#" className="underline underline-offset-4">
                                Terms of Service
                            </Link>{' '}
                            and{' '}
                            <Link to="#" className="underline underline-offset-4">
                                Privacy Policy
                            </Link>
                        </p>
                    </form>
                </div>
            </div>
        </>
    )
}
