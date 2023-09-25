//
//  CouterDownViewModel.swift
//  Pomodoro
//
//  Created by Kagan Kuscu on 10.06.23.
//

import Foundation
import SwiftUI

class CounterDownViewModel: ObservableObject {
    @Published var isActive: Bool = false
    @Published var time: String = "0:00"
    @Published var breakName: String = String(localized: "start-string")
    @Published var countSet: Int = 0
    @Published var isCompleted: Bool = false
    @Published var minutes: Double = 25.0 {
        didSet{
            time = "\(Int(minutes)):00"
        }
    }
    @Published var isShare = false
    @Published var isPaused = false
    
    @AppStorage("longBreak") var longBreak = "15"
    @AppStorage("workTime") var workTime = "25"
    @AppStorage("shortBreak") var shortBreak = "5"
    
    private var initialTime = 0
    private var endDate = Date()
    private var isBreak = false
    private var pomodoro: [Double] = []
    
    func start(minutes: Double) {
        withAnimation(.easeInOut) {
            self.initialTime = Int(minutes)
            self.minutes = minutes
            self.breakName = String(localized: "work")
            self.isActive = true
            self.endDate = Date()
            self.endDate = Calendar.current.date(byAdding: .minute, value: Int(minutes), to: endDate)!
            pomodoro = [Double(workTime) ?? 25.0, Double(shortBreak) ?? 5.0, Double(longBreak) ?? 15.0]
        }
    }
    
    func skip() {
        isBreak.toggle()
        
        if isBreak {
            if self.countSet < 4 {
                self.start(minutes: pomodoro[1])
                withAnimation(.easeInOut) {
                    self.breakName = String(localized: "short-break")
                }
            } else {
                withAnimation(.easeInOut) {
                    self.start(minutes: pomodoro[2])
                    self.breakName = String(localized: "long-break")
                }
            }
            
        } else {
            withAnimation(.easeInOut) {
                self.start(minutes: pomodoro[0])
                self.breakName = String(localized: "work")
            }
        }
        
        if !self.isBreak {
            withAnimation(.easeInOut) {
                self.countSet += 1
            }
        }
        
        if self.countSet > 4 {
            withAnimation(.easeInOut) {
                self.countSet = 0
                self.isCompleted = true
            }
        }
    }
    
    func reset() {
        withAnimation(.easeInOut) {
            self.minutes = 0.0
            self.countSet = 0
            self.breakName = String(localized: "start-string")
            self.isActive = false
            self.isCompleted = false
            self.isPaused = false
        }
    }
    
    func updateCountDown() {
        guard isActive else { return }
        
        let now = Date()
        let diff = endDate.timeIntervalSince1970 - now.timeIntervalSince1970
        
        if diff <= 0 {
            self.skip()
        }
        let date = Date(timeIntervalSince1970: diff)
        let calendar = Calendar.current
        let minutes = calendar.component(.minute, from: date)
        let seconds = calendar.component(.second, from: date)
        
        self.minutes = Double(minutes)
        self.time = String(format: "%d:%02d", minutes, seconds)
    }
}
