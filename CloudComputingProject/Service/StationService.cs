using CloudComputingProject.Infrastructure;
using CloudComputingProject.Model;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingProject.Service;

public interface IStationService
{
    Task<Station?> GetStationById(Guid id);
    Task<ICollection<Station?>> GetAllStations();
    Task<Station> InsertStation(Station station);
    Task<Station> DeleteStation(Guid id);
    Task<Station> UpdateStation(Guid id, Station station);
}

public class StationService : IStationService
{
    private readonly TrainDbContext _context;

    public StationService(TrainDbContext context)
    {
        _context = context;
    }

    public async Task<Station?> GetStationById(Guid id)
    {
        return await _context.Station.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ICollection<Station?>> GetAllStations()
    {
        return await _context.Station.ToListAsync();
    }

    public async Task<Station> InsertStation(Station station)
    {
        station.Id = Guid.NewGuid();
        await _context.Station.AddAsync(station);
        await _context.SaveChangesAsync();
        return station;
    }

    public async Task<Station> DeleteStation(Guid id)
    {
        var station = await _context.Station.FirstOrDefaultAsync(x => x.Id == id);
        if (station == null)
        {
            throw new ArgumentException($"No station with {id} could be found!");
        }
        _context.Station.Remove(station);
        await _context.SaveChangesAsync();
        return station;
    }

    public async Task<Station> UpdateStation(Guid id, Station station)
    {
        var stationStored = await _context.Station.FirstOrDefaultAsync(x => x.Id == id);
        if (stationStored == null)
        {
            throw new ArgumentException($"No station with {id} could be found!");
        }
        _context.Station.Update(stationStored);
        stationStored = station;
        await _context.SaveChangesAsync();
        return stationStored;
    }
}