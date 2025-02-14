using BookingManager.Application.Abstractions;
using BookingManager.DAL.Entities;
using BookingManager.DAL.Repositories;

ICustomerRepository customerRepository = new CustomerRepository();
IOptionRepository optionRepository = new OptionRepository();

Option o = optionRepository.GetById(4) ?? throw new Exception();
optionRepository.Remove(o);


