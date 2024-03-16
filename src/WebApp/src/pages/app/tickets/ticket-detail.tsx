import { Button } from '@/components/ui/button'
import {
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
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
import { useMutation } from '@tanstack/react-query'
import { useContext } from 'react'
import { Controller, useForm } from 'react-hook-form'
import { z } from 'zod'

import { Ticket } from '@/models/ticket'
import { updateTicket } from '@/services/update-ticket'
import { toast } from 'sonner'

export interface TicketDetailProps {
  ticket: Ticket
  hasUpdateTicket: () => void
}

const updateTicketForm = z.object({
  subject: z.string(),
  content: z.string(),
  priority: z.string(),
  sectorId: z.string(),
  userId: z.string(),
  assigneeId: z.string(),
})

type UpdateTicketForm = z.infer<typeof updateTicketForm>

export function TicketDetail({ ticket, hasUpdateTicket }: TicketDetailProps) {
  const { sectors, user, users, holder } = useContext(AppContext)

  const { handleSubmit, register, formState, control } =
    useForm<UpdateTicketForm>({
      defaultValues: {
        subject: ticket?.title ?? '',
        content: ticket?.content ?? '',
        priority: ticket?.priority.toString() ?? '',
        sectorId: ticket?.sectorId.toString() ?? '',
        userId: ticket?.userId.toString() ?? '',
        assigneeId: ticket?.assigneeId?.toString() ?? 'Assign a user',
      },
    })

  const { mutateAsync: updateTicketFn } = useMutation({
    mutationFn: updateTicket,
  })

  async function handleUpdateTicket(data: UpdateTicketForm) {
    console.log(data)

    try {
      const request = {
        id: ticket.id,
        title: data.subject,
        content: data.content,
        priority: data.priority,
        sectorId: data.sectorId,
        userId: user.id,
        holderId: holder.id,
        status: ticket.status,
        assigneeId: data?.assigneeId ?? null,
      }

      const response = await updateTicketFn(request)

      if (response.status === 204) {
        toast.success('Ticket updated successfully')
        document.getElementById('update-ticket-cancel')?.click()
        hasUpdateTicket()
      } else {
        toast.error('Something went wrong')
      }
    } catch {
      toast.error('Something went wrong')
    }
  }

  return (
    <>
      <DialogContent onOpenAutoFocus={(e) => e.preventDefault()}>
        <form onSubmit={handleSubmit(handleUpdateTicket)} className="space-y-4">
          <DialogHeader>
            <DialogTitle>Ticket</DialogTitle>
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
                    defaultValue={ticket?.priority.toString()}
                    render={({
                      field: { name, onChange, value, disabled },
                    }) => {
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
                            <SelectGroup defaultValue={ticket?.priority}>
                              {Object.keys(TicketPriority).map((priority) => {
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
                  <Label htmlFor="sector">Sector</Label>
                  <Controller
                    name="sectorId"
                    control={control}
                    defaultValue={ticket?.sectorId?.toString()}
                    render={({
                      field: { name, onChange, value, disabled },
                    }) => {
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
                <Label htmlFor="assigned">Assigned to</Label>
                <Controller
                  name="assigneeId"
                  control={control}
                  defaultValue={
                    ticket?.assigneeId?.toString() ?? 'Assign a user'
                  }
                  render={({ field: { name, onChange, value, disabled } }) => {
                    return (
                      <Select
                        name={name}
                        onValueChange={onChange}
                        value={value}
                        disabled={disabled}
                      >
                        <SelectTrigger className="w-[220px]">
                          <SelectValue placeholder="Assign a user" />
                        </SelectTrigger>
                        <SelectContent>
                          <SelectGroup>
                            {users.map((user) => {
                              return (
                                <SelectItem
                                  key={user.id}
                                  value={user.id}
                                  {...register('assigneeId')}
                                >
                                  {user.name}
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
                <Label htmlFor="content">Message</Label>
                <Textarea id="content" {...register('content')} />
              </div>
            </DialogDescription>
          </DialogHeader>
          <DialogFooter>
            <DialogTrigger asChild>
              <Button variant="outline" id="update-ticket-cancel">
                Cancel
              </Button>
            </DialogTrigger>
            <Button disabled={formState.isSubmitting}>Answer</Button>
            <Button disabled={formState.isSubmitting} type="submit">
              Save
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </>
  )
}
