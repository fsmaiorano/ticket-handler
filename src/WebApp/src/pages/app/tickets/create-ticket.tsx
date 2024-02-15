import { Button } from '@/components/ui/button'
import {
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
} from '@/components/ui/dialog'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import { Textarea } from '@/components/ui/textarea'
import { AppContext } from '@/contexts/app-context'
import { TicketPriority } from '@/models/ticket-priority'
import { useContext } from 'react'
import { Controller, useForm } from 'react-hook-form'
import { z } from 'zod'

const createTicketForm = z.object({
  subject: z.string(),
  content: z.string(),
  priority: z.string(),
  sectorId: z.string(),
})

type CreateTicketForm = z.infer<typeof createTicketForm>

export function CreateTicket() {
  const { sectors } = useContext(AppContext)

  const { handleSubmit, register, formState, control } =
    useForm<CreateTicketForm>({
      defaultValues: {
        // email: searchParams.get('email') ?? '',
        // password: searchParams.get('password') ?? '',
      },
    })

  async function handleCreateTicket(data: CreateTicketForm) {
    console.log(data)
    // try {
    //   const response = await authenticate({
    //     email: data.email,
    //     password: data.password,
    //   })
    //   if (response.success) {
    //     userHandler(response.user)
    //     tokenHandler(response.token)
    //     navigate(response.redirectUrl, {
    //       replace: true,
    //       state: { from: '/' },
    //     })
    //     // window.location.href = response.redirectUrl
    //   } else {
    //     toast.error('Something went wrong')
    //   }
    //   // else {
    //   //   toast.success('We send you an email with a link to sign in')
    //   // }
    // } catch {
    //   toast.error('Something went wrong')
    // }
  }

  return (
    <form onSubmit={handleSubmit(handleCreateTicket)} className="space-y-4">
      <DialogHeader>
        <DialogTitle>Create Ticket</DialogTitle>
        <DialogDescription>
          <div className="mt-5 space-y-3">
            <Label className="left" htmlFor="subject">
              Subject
            </Label>
            <Input id="subject" type="text" {...register('subject')} />
          </div>
          <div className="flex flex-row justify-between">
            <div className="mt-5 space-y-3">
              <Label htmlFor="email">Priority</Label>
              <Controller
                name="priority"
                control={control}
                render={({ field: { name, onChange, value, disabled } }) => {
                  return (
                    <Select
                      name={name}
                      onValueChange={onChange}
                      value={value}
                      disabled={disabled}
                    >
                      <SelectTrigger className="w-[220px]">
                        <SelectValue placeholder="Select the priority" />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectGroup>
                          {Object.keys(TicketPriority)
                            .filter((value) => isNaN(Number(value)))
                            .map((priority) => {
                              return (
                                <SelectItem
                                  key={priority}
                                  value={priority}
                                  {...register('priority')}
                                >
                                  {priority}
                                </SelectItem>
                              )
                            })}
                        </SelectGroup>
                      </SelectContent>
                    </Select>
                  )
                }}
              ></Controller>
            </div>
            <div className="mt-5 space-y-3">
              <Label htmlFor="email">Sector</Label>
              <Controller
                name="sectorId"
                control={control}
                render={({ field: { name, onChange, value, disabled } }) => {
                  return (
                    <Select
                      name={name}
                      onValueChange={onChange}
                      value={value}
                      disabled={disabled}
                    >
                      <SelectTrigger className="w-[220px]">
                        <SelectValue placeholder="Select an sector" />
                      </SelectTrigger>
                      <SelectContent>
                        <SelectGroup>
                          {sectors.map((sector) => {
                            return (
                              <SelectItem
                                key={sector.id}
                                value={sector.id}
                                {...register('sectorId')}
                              >
                                {sector.name}
                              </SelectItem>
                            )
                          })}
                        </SelectGroup>
                      </SelectContent>
                    </Select>
                  )
                }}
              ></Controller>
            </div>
          </div>
          <div className="mt-5 space-y-3">
            <Label htmlFor="content">Message</Label>
            <Textarea id="content" {...register('content')} />
          </div>
        </DialogDescription>
      </DialogHeader>
      <DialogFooter>
        <Button disabled={formState.isSubmitting} type="submit">
          Confirm
        </Button>
      </DialogFooter>
    </form>
  )
}
